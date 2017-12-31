using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{

    public class MongoPipelineOutput
    {
        public object _id { get; set; }
        public Position[] Positions { get; set; }
    }

    public class Position
    {
        public string _id { get; set; }
        public Set[] Sets { get; set; }
    }

    public class Set
    {
        public SetId _id { get; set; }
        public float winRate { get; set; }
        public float kills { get; set; }
        public float visionScore { get; set; }
        public float controlScore { get; set; }
        public float turretsDamage { get; set; }
        public float mitigatedDamage { get; set; }
        public float dealtDamage { get; set; }
        public float takenDamage { get; set; }
        public float goldEarned { get; set; }
        public float heal { get; set; }
        public float kda { get; set; }
        public int count { get; set; }
    }

    public class SetId
    {
        public string runeIdentifier { get; set; }
        public string lane { get; set; }
    }
}
