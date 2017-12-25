<template>
<div class="select-wrapper">
   <Search />
  <div class="select-champion" v-if="!selectedChampion">
    <h1>Select champion</h1>
    <div class="champions-grid">
      <div class="champion" v-for="champion in champions" @click="selectChampion(champion)" :key="champion.name">{{ champion.name }}

      </div>
    </div>
  </div>
  <div class="select-lane" v-else>
    <h1>Select lane</h1>
    <div class="lanes-grid">
      <div class="lane" v-for="lane in lanes" @click="selectLane(lane)" :key="lane">
        <img :src="lanePath(lane)" :alt="lane">
      </div>
    </div>
  </div>
  <Runes />
</div>
</template>

<script>
import Runes from '@/components/Runes'
import Search from '@/components/Search'
import champions from '@/data/champion.json';

console.log(champions.data)

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
      lanes: ['top', 'jungler', 'mid', 'bot'],
    }
  },
  mounted() {
    console.log(this.runes)
  },
  methods: {
    selectChampion(champion) {
      this.selectedChampion = champion.id;
    },
    selectLane(lane) {
      this.selectedLane = lane;
    },
    selectRune(rune) {
      this.selectedRune = rune.id;
    },
    lanePath(lane) {
      return require(`../assets/lanes/${lane}.png`);
    }
  }
}
</script>
<style lang="stylus">
.champions-grid
  display grid
  grid-template-columns repeat(10, 1fr)

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


