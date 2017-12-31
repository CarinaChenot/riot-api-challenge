<template>
<div class="select-wrapper">
  <Search v-on:selectedChamp="selectChampion" v-if="!selectedChampion"/>
   <div>
     <img v-if="selectedChampion" :src="championPath(selectedChampion)" :alt="selectedChampion"  class="selectedChampion" @click="selectedChampion = null; selectedLane = null">  
     <img class="selectedLane" v-if="selectedLane" :src="lanePath(selectedLane)" :alt="selectedLane" @click="selectedLane = null">
    </div>
  <div class="select-lane" v-if="selectedChampion && !selectedLane">
    <h1>Select your lane</h1>
    <div class="lanes-grid">
      <div class="lane" v-for="lane in lanes" @click="selectLane(lane)" :key="lane">
        <img :src="lanePath(lane)" :alt="lane">
      </div>
    </div>
  </div>
  <Runes v-if="selectedChampion && selectedLane" v-on:runeSetSelected="loadStats" />
</div>
</template>

<script>
import Runes from '@/components/Runes'
import Search from '@/components/Search'
import champions from '@/data/champion.json'
import * as alertify from 'alertifyjs';

require('alertifyjs/build/css/alertify.min.css')
require('alertifyjs/build/css/themes/semantic.min.css')

export default {
  components: {
    Runes,
    Search,
  },
  data() {
    return {
      champions: champions.data,
      selectedChampion: null,
      selectedLane: null,
      selectedRune: null,
      lanes: ['TOP', 'JUNGLE', 'MIDDLE', 'BOTTOM', 'SUPPORT'],
      stats: null,
      runeSetStats: null,
    }
  },
  mounted() {
  },
  methods: {
    selectChampion(champion) {
      this.selectedChampion = champion;
    },
    selectLane(lane) {
      this.selectedLane = lane;
      this.getStats();
    },
    selectRune(rune) {
      this.selectedRune = rune.id;
    },
    lanePath(lane) {
      return require(`../assets/lanes/${lane}.png`);
    },
    championPath(champ) {
      return require(`../assets/champions/${champ}.png`);
    },      
    getStats() {
      let key = this.champions[this.selectedChampion].key;
      let url = `../src/assets/stats/${key}.json`;

      fetch(url)
        .then((response) => {
          return response.json();
        })
        .then((json) => {
          this.stats = this.getSets(json);
        });
    },
    getSets(statsJson) {
      const index = statsJson.Positions.findIndex(p => p._id == this.selectedLane)

      if(index == -1){
          alertify.alert ("Oops!", "We don't have any data for " + this.selectedChampion + " " + this.selectedLane);
      }
      else{
          this.$emit('runeSetsLoaded', statsJson.Positions[index].Sets);
          return statsJson.Positions[index].Sets;
      }
    },
    loadStats(runeIdentifier) {
      // Si ça renvoie -1 c'est que y'a pas de data pour ce runeSet (ou alors j'ai merdé qq part)
      this.runeSetStats = this.stats.find(p => p._id.runeIdentifier === runeIdentifier)
      
      if(!this.runeSetStats){
          alertify.alert ("Oops!", "We don't have any data for that runes set");
      }
      else
        this.$emit('runeSetSelected', this.runeSetStats._id.runeIdentifier);
    }
  }
}
</script>

<style lang="stylus">
.select-wrapper
  display flex
  flex-direction column
  justify-content center
  align-items center

.champions-grid
  display grid
  grid-template-columns repeat(10, 1fr)

.selectedLane, .selectedChampion
  width:55px;


.select-lane
  h1
    text-align center
.lanes-grid
  display flex

.lane
  height 55px
  width 55px
  cursor pointer

  img
    width 100%
    height 100%
</style>


