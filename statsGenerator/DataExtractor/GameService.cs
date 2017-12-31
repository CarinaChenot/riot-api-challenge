using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Jil;
using LolAPI;
using LolAPI.DTO;
using log4net;
using MongoDB.Driver;
using System.Reflection;
using MongoDB.Bson.Serialization;

namespace DataExtractor
{
    public class GameService
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(GameService));

        public const long SEED_ACCOUNT_ID = 203243377;
        public const string SUMMONERS_QUEUE_FILE = "summoners.queue";
        private static DateTime DATE_RUNE_REFORGED = new DateTime(2017, 11, 13);
        private readonly LoLApi _api;
        private IMongoCollection<MatchDto> _coll;

        static GameService()
        {
            foreach (var type in typeof(LoLApi).GetTypeInfo().Assembly.GetTypes())
            {
                if (type.Namespace != null && type.Namespace.EndsWith("DTO"))
                {
                    var map = new BsonClassMap(type);
                    map.AutoMap();
                    BsonClassMap.RegisterClassMap(map);
                }
            }
        }

        public GameService(LoLApi api)
        {
            _api = api;
            var client = new MongoClient(Program.Configuration["MONGO_SERVER"]);
            var db = client.GetDatabase(Program.Configuration["MONGO_DB"]);
            _coll = db.GetCollection<MatchDto>(Program.Configuration["MONGO_COLLECTION"]);
        }

        public async Task Crawl(CancellationToken token)
        {
            SummonerQueue summonerQueue;

            if (File.Exists(SUMMONERS_QUEUE_FILE))
            {
                _logger.Info("Read existing queue");
                summonerQueue = await SummonerQueue.ReadFromFile(SUMMONERS_QUEUE_FILE);
            }
            else
            {
                _logger.Info("Create new queue");
                summonerQueue = new SummonerQueue();
                summonerQueue.Add(SEED_ACCOUNT_ID);
            }

            long accountId;

            while ((accountId = summonerQueue.Get()) != 0)
            {
                _logger.InfoFormat("{0} summoners to crawl. Current : {1}", summonerQueue.Count, summonerQueue.Index);
                _logger.InfoFormat("Query information about summoner {0}", accountId);

                if(summonerQueue.Index % 20 == 0)
                {
                    await summonerQueue.SaveToFile(SUMMONERS_QUEUE_FILE);
                    _logger.Info("Summoner queue successfully saved");
                }

                MatchlistDto accountMatches;

                try
                {
                    accountMatches = await _api.GetMatchsForAccount(accountId, DATE_RUNE_REFORGED);
                }
                catch (Exception ex)
                {
                    _logger.WarnFormat("Error while querying about summoner {0} : {1}", accountId, ex);
                    continue;
                }

                if (accountMatches?.matches == null) continue;

                //We browse all of his matchs
                foreach (var match in accountMatches.matches)
                {
                    if (token.IsCancellationRequested)
                    {
                        await summonerQueue.SaveToFile(SUMMONERS_QUEUE_FILE);
                        _logger.Info("Summoner queue successfully saved");
                        return;
                    }

                    MatchDto dbMatch = (await _coll.FindAsync(m=> m.gameId == match.gameId)).FirstOrDefault();

                    if (dbMatch == null)
                    {
                        //Query match info
                        try
                        {
                            var matchInfo = await _api.GetMatch(match.gameId);

                            foreach (var identity in matchInfo.participantIdentities)
                                summonerQueue.Add(identity.player.accountId);

                            await _coll.InsertOneAsync(matchInfo);
                        }
                        catch (Exception ex)
                        {
                            _logger.WarnFormat("Error while querying about match {0} : {1}", match.gameId, ex);
                        }                     
                    }
                    else
                    {
                        _logger.InfoFormat("Data for game {0} already exists", match.gameId);
                    }
                }
            }
        }

        private class SummonerQueue
        {
            private int _index;
            private readonly List<long> _list = new List<long>();

            public void Add(long summonerId)
            {
                if (!_list.Contains(summonerId))
                {
                    _list.Add(summonerId);
                }
            }

            public int Index => _index;
            public int Count => _list.Count;
            public bool IsEmpty => _index >= _list.Count;

            public long Get()
            {
                if (IsEmpty)
                    return 0;

                return _list[_index++];
            }

            public static async Task<SummonerQueue> ReadFromFile(string filename)
            {
                string indexName = filename + ".index";
                var queue = new SummonerQueue();

                if (File.Exists(indexName))
                {
                    queue._index = int.Parse(File.ReadAllText(indexName));
                }

                using (var reader = new StreamReader(File.OpenRead(filename)))
                {
                    while (!reader.EndOfStream)
                    {
                        queue._list.Add(long.Parse(await reader.ReadLineAsync()));
                    }
                }
                return queue;
            }

            public async Task SaveToFile(string filename)
            {
                string indexName = filename + ".index";

                File.WriteAllText(indexName, _index.ToString());

                using (var writer = new StreamWriter(File.Create(filename)))
                {
                    foreach (long summonerId in _list)
                        await writer.WriteLineAsync(summonerId.ToString());
                }
            }
        }
    }
}
