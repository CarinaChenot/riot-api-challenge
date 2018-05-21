<template>
  <ais-index :search-store="searchStore" index-name="champions">
    <h1>Select your champion</h1>
    <ais-search-box placeholder="champion name" :autofocus="true"></ais-search-box>
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

const searchStore = createFromAlgoliaCredentials('APP_NAME', 'APP_TOKEN');

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

.ais-input
  border: black 1px solid;
  background: #fbdea8;
  border-radius: 20px;
  height: 30px
  width:80%
  padding: 4px 10px;
  font-weight: bold;
  font-family: 'friz quadrata';
  
.ais-input:focus
  outline: none

.ais-search-box__submit, .ais-clear
  display:none

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
  overflow-y: auto;
  margin-top:5px
  max-height: 400px;

.result_champion
  display flex
  justify-content space-between
  align-items center
  cursor:pointer
  padding-left:8px

.result_champion:hover
  background: #FBDEA8
  color: black

.champion_square
  max-height 50px
</style>
