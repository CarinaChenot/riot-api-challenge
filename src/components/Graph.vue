<template>
<div class="radarChart"></div>
</template>

<script>
    import * as d3 from 'd3';
    import RadarChart from '@/components/radarChart';

    var axes = [
        {
            name: "WinRate",
            legend: null
        },
        {
            name: "KDA",
            legend: null
        },
        {
            name: "Kills",
            legend: null
        },
        {
            name: "Vision",
            legend: null
        },
        {
            name: "Crowd Control",
            legend: null
        },
        {
            name: "Map Objectives",
            legend: null
        },
        {
            name: "Golds",
            legend: null
        },
        {
            name: "Damages",
            legend: null
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

	function draw(sets, s){
		
		var b = getMinMax(sets);
		
		var d = [
                (s.winRate - b.min.winRate) / (b.max.winRate - b.min.winRate),
                (s.kda - b.min.kda) / (b.max.kda - b.min.kda),
                (s.kills - b.min.kills) / (b.max.kills - b.min.kills),
                (s.visionScore - b.min.visionScore) / (b.max.visionScore - b.min.visionScore),
                (s.controlScore - b.min.controlScore) / (b.max.controlScore - b.min.controlScore),
                (s.turretsDamage - b.min.turretsDamage) / (b.max.turretsDamage - b.min.turretsDamage),
                (s.goldEarned - b.min.goldEarned) / (b.max.goldEarned - b.min.goldEarned),
                (s.dealtDamage - b.min.dealtDamage) / (b.max.dealtDamage - b.min.dealtDamage),
                (s.takenDamage - b.min.takenDamage) / (b.max.takenDamage - b.min.takenDamage)
            ];
			
		radar.setData([d]);
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
      	  sets: function(val) {
             draw(this.sets, this.selectedSets);
          },
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
text-shadow: 0 1px 0 #fff,
1px 0 0 #fff,
-1px 0 0 #fff,
0 -1px 0 #fff;
cursor: default;

}

.legend {font-family: Georgia,
serif; //Impact, Charcoal, sans-serif;
fill: #333333;}

.tooltip {fill: #333333;}

div.tooltip {
position: absolute;
text-align: center;
width: 60px;
height: 28px;
padding: 2px;
font: 12px sans-serif;
background: lightsteelblue;
border: 0px;
border-radius: 8px;
pointer-events: none;
}
</style>