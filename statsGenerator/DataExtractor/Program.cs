using System;
using System.Threading;
using LolAPI;
using Jil;
using System.Xml;
using System.Reflection;
using log4net.Repository.Hierarchy;
using log4net.Config;
using System.IO;
using MongoDB.Driver;
using LolAPI.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Linq;
using LolAPI.Static;
using Newtonsoft.Json;
using log4net;
using Microsoft.Extensions.Configuration;

namespace DataExtractor
{
    class Program
    {
        public static string STATS_DATA_FOLDER;
        public static IConfigurationRoot Configuration;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            ConfigureLogs();

            STATS_DATA_FOLDER = Configuration["STATS_DATA_FOLDER"];
            var api = new LoLApi(Configuration["API_KEY"]);

            JSON.SetDefaultOptions(new Options(false, true));

            Console.ReadLine();
        }

        static void ConfigureLogs()
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));
            var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(Hierarchy));
            XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }

        static void CrawlGames(LoLApi api)
        {
            var tokenSource = new CancellationTokenSource();
            var service = new GameService(api);
            var task = service.Crawl(tokenSource.Token);

            Console.ReadLine();

            tokenSource.Cancel();
            task.Wait();
        }

        static void ToMongoDB()
        {
            var client = new MongoClient(Configuration["MONGO_SERVER"]);
            var db = client.GetDatabase(Configuration["MONGO_DB"]);
            var coll = db.GetCollection<MatchDto>(Configuration["MONGO_COLLECTION"]);

            var assembly = typeof(Program).GetTypeInfo().Assembly;

            var champions = JSON.Deserialize<StaticData>(File.ReadAllText(Configuration["CHAMPIONS_DATA"]));

            using (var reader = new StreamReader(assembly.GetManifestResourceStream("DataExtractor.mongoPipeline.js")))
            {
                string mongoPipeline = reader.ReadToEnd();

                foreach(var champion in champions.data.Values)
                {
                    _logger.Info("Generating stats for " + champion.name);

                    string content = mongoPipeline.Replace("%ChampionId%", champion.key);
                    var pipeline = BsonSerializer.Deserialize<BsonDocument[]>(content);
                    var result = coll.Aggregate<BsonDocument>(pipeline, new AggregateOptions() { AllowDiskUse = true }).FirstOrDefault();

                    File.WriteAllText(Path.Combine(STATS_DATA_FOLDER, champion.key + ".json"), result.ToJson());
                }             
            }    
        }



        ////input (champion + lane)
        ////output =>
        ////5 combinaisons les plus utilisés avec winrate
        ////quand on clique dessus ou creer propre rune => affiche les stats moyenne pour cette combinaison
        ////sup quand moins de 50 to ensure good stats

    }
}
