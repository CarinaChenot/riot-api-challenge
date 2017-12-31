using System;
using System.Threading.Tasks;
using LolAPI.DTO;

namespace LolAPI
{
    public class LoLApi
    {
        private readonly ApiHttpClient _httpClient;
        private static TimeSpan OneDay = TimeSpan.FromDays(1);
        private static TimeSpan OneWeek = TimeSpan.FromDays(7);
        private static TimeSpan OnMonth = TimeSpan.FromDays(30.5);
        private static TimeSpan ThirtyMin = TimeSpan.FromMinutes(30);
        private static TimeSpan Forever = TimeSpan.FromDays(3650);

        public LoLApi(string key)
        {
            _httpClient = new ApiHttpClient(key);
        }

        #region Summoner-V3

        public async Task<SummonerDTO> GetSummonerById(long summonerId)
        {
            return await _httpClient.Get<SummonerDTO>($"/lol/summoner/v3/summoners/{summonerId}", OneWeek);
        }

        public async Task<SummonerDTO> GetSummonerByAccount(long accountId)
        {
            return await _httpClient.Get<SummonerDTO>($"/lol/summoner/v3/summoners/by-account/{accountId}", OneWeek);
        }

        public async Task<SummonerDTO> GetSummonerByName(string summonerName)
        {
            return await _httpClient.Get<SummonerDTO>($"/lol/summoner/v3/summoners/by-name/{summonerName}", OneWeek);
        }

        #endregion

        #region Runes-V3

        /// <summary>
        /// Get rune pages for a given summoner ID.
        /// </summary>
        /// <param name="summonerId"></param>
        /// <returns></returns>
        public async Task<RunePagesDto> GetSummonerRunes(long summonerId)
        {
            return await _httpClient.Get<RunePagesDto>($"/lol/platform/v3/runes/by-summoner/{summonerId}", OneWeek);
        }

        #endregion

        #region Masteries-V3

        /// <summary>
        /// Get mastery pages for a given summoner ID.
        /// </summary>
        /// <param name="summonerId"></param>
        /// <returns></returns>
        public async Task<MasteryPagesDto> GetSummonerMasteries(long summonerId)
        {
            return await _httpClient.Get<MasteryPagesDto>($"/lol/platform/v3/masteries/by-summoner/{summonerId}", OneWeek);
        }

        #endregion

        #region ChampionMastery-V3

        /// <summary>
        /// Get all champion mastery entries sorted by number of champion points descending.
        /// </summary>
        /// <param name="summonerId"></param>
        /// <returns></returns>
        public async Task<ChampionMasteryDTO[]> GetSummonerChampionsMastery(long summonerId)
        {
            return await _httpClient.Get<ChampionMasteryDTO[]>($"/lol/champion-mastery/v3/champion-masteries/by-summoner/{summonerId}", OneWeek);
        }

        /// <summary>
        /// Get a champion mastery by player ID and champion ID.
        /// </summary>
        /// <param name="summonerId"></param>
        /// <param name="championId"></param>
        /// <returns></returns>
        public async Task<ChampionMasteryDTO> GetSummonerChampionMastery(long summonerId, int championId)
        {
            return await _httpClient.Get<ChampionMasteryDTO>($"/lol/champion-mastery/v3/champion-masteries/by-summoner/{summonerId}/by-champion/{championId}", OneWeek);
        }

        /// <summary>
        /// Get a player's total champion mastery score, which is the sum of individual champion mastery levels
        /// </summary>
        /// <param name="summonerId"></param>
        /// <param name="championId"></param>
        /// <returns></returns>
        public async Task<int> GetSummonerChampionsMasteryScore(long summonerId, int championId)
        {
            return await _httpClient.Get<int>($"/lol/champion-mastery/v3/scores/by-summoner/{summonerId}", OneWeek);
        }

        #endregion

        #region Match-V3

        /// <summary>
        /// Get match by match ID
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public async Task<MatchDto> GetMatch(long matchId)
        {
            return await _httpClient.Get<MatchDto>($"/lol/match/v3/matches/{matchId}", Forever, "matchs");
        }

        /// <summary>
        /// Get matchlist for ranked games played on given account ID
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="beginIndex"></param>
        /// <returns></returns>
        public async Task<MatchlistDto> GetRankedMatchsForAccount(long accountId, int beginIndex = 0)
        {
            if (beginIndex > 0)
                return await _httpClient.Get<MatchlistDto>($"/lol/match/v3/matchlists/by-account/{accountId}?beginIndex={beginIndex}", OneWeek);

            return await _httpClient.Get<MatchlistDto>($"/lol/match/v3/matchlists/by-account/{accountId}", OneWeek, "matchlists");
        }

        public async Task<MatchlistDto> GetRankedMatchsForAccountAndChampion(long accountId, int championId)
        {
            return await _httpClient.Get<MatchlistDto>($"/lol/match/v3/matchlists/by-account/{accountId}?champion={championId}", OneWeek);
        }


        /// <summary>
        /// Get matchlist for ranked games played on given account ID
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="season"></param>
        /// <returns></returns>
        public async Task<MatchlistDto> GetRankedMatchsForAccountForSeason(long accountId, int season)
        {
            return await _httpClient.Get<MatchlistDto>($"/lol/match/v3/matchlists/by-account/{accountId}?season={season}", OneWeek);
        }


        /// <summary>
        /// Get matchlist for last 20 matches played on given account ID and platform ID.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<MatchlistDto> GetMatchsForAccount(long accountId)
        {
            return await _httpClient.Get<MatchlistDto>($"/lol/match/v3/matchlists/by-account/{accountId}/recent", OneDay, "matchlists");
        }


        /// <summary>
        /// Get matchlist for last 20 matches played on given account ID and platform ID.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<MatchlistDto> GetMatchsForAccount(long accountId, DateTime dateDebut)
        {
            long beginTime = ToUnixTimeStamp(dateDebut);

            return await _httpClient.Get<MatchlistDto>($"/lol/match/v3/matchlists/by-account/{accountId}?beginTime={beginTime}&queue=400&queue=420&queue=430&queue=440", OneDay, "matchlists");
        }


        private long ToUnixTimeStamp(DateTime dt)
        {
            return (long)(dt - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        /// <summary>
        /// Get match timeline by match ID
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public async Task<MatchTimelineDto> GetMatchTimeline(long matchId)
        {
            return await _httpClient.Get<MatchTimelineDto>($"/lol/match/v3/timelines/by-match/{matchId}", Forever, "matchs");
        }

        #endregion

        #region League-V3

        /// <summary>
        /// Get league positions in all queues for a given summoner ID
        /// </summary>
        /// <param name="summonerId"></param>
        /// <returns></returns>
        public async Task<LeaguePositionDTO[]> GetLeaguesPosition(long summonerId)
        {
            return await _httpClient.Get<LeaguePositionDTO[]>($"/lol/league/v3/positions/by-summoner/{summonerId}", OneWeek, "leagues");
        }

        #endregion

    }
}
