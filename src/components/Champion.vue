<template>
<div class="select-wrapper">
  <div class="select-champion">
    <h1>Select champion</h1>
    <div class="champions-grid">
      <div class="champion" v-for="champion in champions" @click="selectChampion(champion)" :key="champion.name">{{ champion.name }}</div>
    </div>
  </div>
  <div class="select-lane">
    <h1>Select lane</h1>
    <div class="lanes-grid">
      <div class="lane" v-for="lane in lanes" @click="selectLane(lane)" :key="lane">{{ lane }}</div>
    </div>
  </div>
  <Runes />
</div>
</template>

<script>
import Runes from '@/components/Runes'
import champions from '@/data/champion.json';

export default {
  components: {
    Runes,
  },
  data() {
    return {
      champions: champions.data,
      selectedChampion: null,
      selectedLane: null,
      selectedRune: null,
      lanes: ['top', 'jungle', 'mid', 'bottom', 'support'],
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
    }
  }
}
</script>
<style>
.champions-grid {
  display: grid;
  grid-template-columns: repeat(10, 1fr);
}
</style>


