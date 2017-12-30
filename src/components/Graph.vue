<template>
<div>   
    <div class="radarChart"></div>
    <span class="md-body-2">Select your champion, your lane and your runes to see what can bring you</span>
</div>
</template>

<script>
    import * as d3 from 'd3';
    import RadarChart from '@/components/radarChart';

    var axes = [
        {
            name: "WinRate",
            legend: "The average WinRate for that runes set"
        },
        {
            name: "KDA",
            legend: null
        },
        {
            name: "Kills",
            legend: "The poor souls you left behind"
        },
        {
            name: "Vision",
            legend: "How much vision you put on the map. Put a ward, save an ally"
        },
        {
            name: "Crowd Control",
            legend: "How much crow control you apply on the opponents. Preventing them to reach your allies !"
        },
        {
            name: "Map Objectives",
            legend: null
        },
        {
            name: "Golds",
            legend: "Golds won during the game"
        },
        {
            name: "Damages",
            legend: "Average damage dealt on the opponents"
        },
        {
            name: "Tanking",
            legend: null
        },
    ];

	var radar = null;
	
    function initChart() {

        //Call function to draw the Radar chart
        radar = new RadarChart(".radarChart", axes, {w:250, h:250});
    }

	function draw(sets, selectedSets){	
		var data = getChartStats(sets, selectedSets.map(s => sets.find(s2 => s == s2._id.runeIdentifier)));

		radar.setData(data);
	}
	
	function getChartStats(sets, selectedSets){
	
		var bounds = getMinMax(sets);
		var data = [];
		
		for(var set of selectedSets){
			data.push([
				{legend:(100 * set.winRate).toFixed(1) + '%', value:(set.winRate - bounds.min.winRate) / (bounds.max.winRate - bounds.min.winRate)},
				{legend:set.kda.toFixed(1) + ' KDA', value:(set.kda - bounds.min.kda) / (bounds.max.kda - bounds.min.kda)},
				{legend:set.kills.toFixed(1) + ' kills', value:(set.kills - bounds.min.kills) / (bounds.max.kills - bounds.min.kills)},
                {legend:Math.round(set.visionScore)  + ' ward points', value:(set.visionScore - bounds.min.visionScore) / (bounds.max.visionScore - bounds.min.visionScore)},
                {legend:Math.round(set.controlScore)+ ' seconds', value:(set.controlScore - bounds.min.controlScore) / (bounds.max.controlScore - bounds.min.controlScore)},
                {legend:Math.round(set.turretsDamage) + ' damage', value:(set.turretsDamage - bounds.min.turretsDamage) / (bounds.max.turretsDamage - bounds.min.turretsDamage)},
                {legend:Math.round(set.goldEarned) + ' gold', value:(set.goldEarned - bounds.min.goldEarned) / (bounds.max.goldEarned - bounds.min.goldEarned)},
                {legend:Math.round(set.dealtDamage) + ' damage', value:(set.dealtDamage - bounds.min.dealtDamage) / (bounds.max.dealtDamage - bounds.min.dealtDamage)},
                {legend:Math.round(set.takenDamage) + ' damage', value:(set.takenDamage - bounds.min.takenDamage) / (bounds.max.takenDamage - bounds.min.takenDamage)}
            ]);
		}		
		return data;
	}
	
    function getMinMax(sets){
        var min = Object.assign({}, sets[0]);
        var max = Object.assign({}, sets[0]);

        sets.map(function(s){
            for(var prop in s){          
                if(min[prop] > s[prop]) min[prop] = s[prop];
                if(max[prop] < s[prop]) max[prop] = s[prop];
            }
        });

        return{min: min, max:max};
    }



    window.onload = initChart;


    export default {
		props: [
			'sets',
			'selectedSets',
		],
		watch: { 
		  selectedSets: function(val) {
             draw(this.sets, this.selectedSets);
          }
      }
    }
</script>

<style lang="stylus">

.radarChart {
font-size: 11px;
font-weight: 300;
fill: #242424;
text-align: center;
/*text-shadow: 0 1px 0 #fff,
1px 0 0 #fff,
-1px 0 0 #fff,
0 -1px 0 #fff;*/
cursor: default;

}

.legend {font-family: Georgia,
serif; //Impact, Charcoal, sans-serif;
fill: white;}

//.tooltip {fill: #333333;}

div.tooltip {
position: absolute;
text-align: center;
min-height: 15px;
min-width: 40px;
padding: 4px 8px;
font: 12px sans-serif;
text-shadow: none;
//background: lightsteelblue;
background:black;
color:white;
border: 0px;
border-radius: 8px;
pointer-events: none;
}
</style>