namespace LolAPI.DTO
{
    public class MatchEventDto
    {
        public int timestamp { get; set; }
        public int skillSlot { get; set; }
        public string levelUpType { get; set; }
        public string type { get; set; }
        public int participantId { get; set; }
        public int itemId { get; set; }
        public int afterId { get; set; }
        public int beforeId { get; set; }
        public int creatorId { get; set; }
        public string wardType { get; set; }
        public int killerId { get; set; }
        public int victimId { get; set; }
        public int?[] assistingParticipantIds { get; set; }
        public MatchPositionDto position { get; set; }
        public string buildingType { get; set; }
        public string towerType { get; set; }
        public int teamId { get; set; }
        public string laneType { get; set; }
        public string monsterType { get; set; }
        public string monsterSubType { get; set; }
    }
}