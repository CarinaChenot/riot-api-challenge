<template>
  <ais-index :search-store="searchStore" index-name="champions">
    <h1>Select your champion</h1>
    <ais-search-box></ais-search-box>
    <ais-results v-show="searchStore._helper.state.query.length > 0">
      <template slot-scope="{ result }">
        <div class="result_champion" @click="selectChamp(result)">
          <ais-highlight :result="result" attribute-name="body"></ais-highlight>
          <img class="champion_square" :src="championPath(result.body)" alt="">
        </div>
      </template>
    </ais-results>
  </ais-index>
</template>

<script>
import { createFromAlgoliaCredentials } from 'vue-instantsearch';

const searchStore = createFromAlgoliaCredentials('6DZDRJMUEP', '081dc30f8b961197b7fe2b93cf18e4e9');

export default {
  data() {
    return { searchStore };
  },
  methods: {
    championPath(champ) {
      return require(`../assets/champions/${champ}.png`);
    },
    selectChamp(champ) {
      this.$emit('selectedChamp', champ.body)
    }
  }
}
</script>

<style lang="stylus">
.ais-index
  max-width 500px

  form
    display flex
    justify-content center

.ais-results
  display flex
  flex-direction column
  box-shadow: 0 5px 100px rgba(37,43,51,.1)
  max-width: 400px;
  margin: 0 auto;

.result_champion
  display flex
  justify-content space-between
  align-items center

.champion_square
  max-height 50px
  // max-height 20px
</style>
