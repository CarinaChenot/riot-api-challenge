namespace LolAPI.DTO
{
    public class ParticipantTimelineDto
    {
        public string lane { get; set; }
        public int participantId { get; set; }
        public Csdiffpermindeltas csDiffPerMinDeltas { get; set; }
        public Goldpermindeltas goldPerMinDeltas { get; set; }
        public Xpdiffpermindeltas xpDiffPerMinDeltas { get; set; }
        public Creepspermindeltas creepsPerMinDeltas { get; set; }
        public Xppermindeltas xpPerMinDeltas { get; set; }
        public string role { get; set; }
        public Damagetakendiffpermindeltas damageTakenDiffPerMinDeltas { get; set; }
        public Damagetakenpermindeltas damageTakenPerMinDeltas { get; set; }
    }
}