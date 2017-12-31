using System.Collections.Generic;

namespace LolAPI
{
    public class Region
    {
        public static Region BR = new Region("BR", "BR1", "br1.api.riotgames.com");
        public static Region EUNE = new Region("EUNE", "EUN1", "eun1.api.riotgames.com");
        public static Region EUW = new Region("EUW", "EUW1", "euw1.api.riotgames.com");
        public static Region JP = new Region("JP", "JP1", "jp1.api.riotgames.com");
        public static Region KR = new Region("KR", "KR", "kr.api.riotgames.com");
        public static Region LAN = new Region("LAN", "LA1", "la1.api.riotgames.com");
        public static Region LAS = new Region("LAS", "LA2", "la2.api.riotgames.com");
        public static Region NA = new Region("NA", "NA1, NA *", "na1.api.riotgames.com");
        public static Region OCE = new Region("OCE", "OC1", "oc1.api.riotgames.com");
        public static Region TR = new Region("TR", "TR1", "tr1.api.riotgames.com");
        public static Region RU = new Region("RU", "RU", "ru.api.riotgames.com");
        public static Region PBE = new Region("PBE", "PBE1", "pbe1.api.riotgames.com");

        public IDictionary<string, Region> List = new Dictionary<string, Region>
        {
            {"BR", BR},
            {"EUNE", EUNE},
            {"EUW", EUW},
            {"JP", JP},
            {"KR", KR},
            {"LAN", LAN},
            {"LAS", LAS},
            {"NA", NA},
            {"OCE", OCE},
            {"TR", TR},
            {"RU", RU},
            {"PBE", PBE},
        };

        public string RegionName { get; }
        public string PlateformName { get; }
        public string Host { get; }

        public Region(string regionName, string plateformName, string host)
        {
            RegionName = regionName;
            PlateformName = plateformName;
            Host = host;
        }
    }
}
