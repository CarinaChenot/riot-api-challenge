<template>

<div class="select-wrapper">
  <Search />
  {{ toto }}
  <div class="select-champion">
    <ais-index
      app-id="latency"
      api-key="3d9875e51fbd20c7754e65422f7ce5e1"
      index-name="bestbuy"
    >
    <ais-search-box></ais-search-box>
    <ais-results>
      <template slot-scope="{ result }">
        <h2>
          <ais-highlight :result="result" attribute-name="name"></ais-highlight>
        </h2>
      </template>
    </ais-results>
  </ais-index>
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
import Search from '@/components/Search'
import champions from '@/data/champion.json';

const algoliasearch = require('algoliasearch');
// const client = algoliasearch("6DZDRJMUEP", "081dc30f8b961197b7fe2b93cf18e4e9");
const index = client.initIndex('champions');

let champArray = [];

for (name in champions.data) {
  champArray.push({body: name});
}

console.log(champArray)
localStorage.setItem('champ', JSON.stringify(champArray))
// index.addObjects(champArray, function(err, content) {
//   if (err) {
//     console.error(err);
//   }
// });

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
      toto: champArray,
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


