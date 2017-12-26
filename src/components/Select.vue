<template>
<div class="select-wrapper">
  <Search v-on:selectedChamp="selectChampion" v-if="!selectedChampion"/>
  <div class="select-lane" v-if="selectedChampion && !selectedLane">
    <h1>Select lane</h1>
    <div class="lanes-grid">
      <div class="lane" v-for="lane in lanes" @click="selectLane(lane)" :key="lane">
        <img :src="lanePath(lane)" :alt="lane">
      </div>
    </div>
  </div>
  <Runes v-if="selectedChampion && selectedLane" />
</div>
</template>

<script>
import Runes from '@/components/Runes'
import Search from '@/components/Search'
import champions from '@/data/champion.json';

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
    getStats() {
      let key = this.champions[this.selectedChampion].key;
      // let url = `http://s477188762.onlinehome.fr/apiChallenge/data/${key}.json`;
      let url = `../static/${key}.json`;

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

      return statsJson.Positions[index].Sets;
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


