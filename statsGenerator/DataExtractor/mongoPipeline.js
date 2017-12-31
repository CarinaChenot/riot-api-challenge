[   

    { $match: { 'participants.championId': %ChampionId% } }, 
    { $match: { 'participants.stats.goldSpent': { $gt: 0 } } },
    { $unwind: { path: '$participants' } }, 
    { $project : { 
        participant: '$participants',
        lane: {
            $cond: {
                if: {
                    $and: [
                        { $eq: ["$participants.timeline.role", "DUO_SUPPORT"] },
                        { $eq: ["$participants.timeline.lane", "BOTTOM"] }]
                },
                then: "SUPPORT",
                else: "$participants.timeline.lane"
            }
        },
        runeIdentifier: { $concat: [  
            {$substr:["$participants.stats.perk0",0,-1]},
            {$substr:["$participants.stats.perk1",0,-1]},
            {$substr:["$participants.stats.perk2",0,-1]},
            {$substr:["$participants.stats.perk3",0,-1]},
            {$substr:["$participants.stats.perk4",0,-1]},
            {$substr:["$participants.stats.perk5",0,-1]}]},
    }},

    { $group: { 
        _id: { runeIdentifier: '$runeIdentifier', lane: '$lane'}, 
        winRate: { $avg: { $cmp: [ "$participant.stats.win", false ] } },
        kills: { $avg : '$participant.stats.kills' },
        visionScore: { $avg : '$participant.stats.visionScore' },
        controlScore: { $avg : '$participant.stats.timeCCingOthers' },
        turretsDamage: { $avg : '$participant.stats.damageDealtToTurrets' },
        mitigatedDamage: { $avg : '$participant.stats.damageSelfMitigated' },
        dealtDamage: { $avg : '$participant.stats.totalDamageDealtToChampions' },
        takenDamage: { $avg : '$participant.stats.totalDamageTaken' },
        goldEarned: { $avg : '$participant.stats.goldEarned' },
        heal: { $avg : '$participant.stats.totalHeal' },
        kda: { $avg: { $divide : [ {$add: [ "$participant.stats.kills", "$participant.stats.assists" ]}, {$cond: { if: { $eq: [ "$participant.stats.deaths", 0 ] }, then: null, else: "$participant.stats.deaths" }} ] }},
        count: { $sum: 1 }
    }},   
    { $match: { count: { $gt: 50 } } }, 
    { $sort: { count : -1 } }, 
    { $group: { 
        _id: '$_id.lane', 
        Sets: { $push: "$$ROOT"  }
    }},
    { $group: { _id: null, Positions: { $push: "$$ROOT"  } } }
]

