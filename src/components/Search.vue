<template>
  <ais-index
    app-id="6DZDRJMUEP"
    api-key="081dc30f8b961197b7fe2b93cf18e4e9"
    index-name="champions"
  >
    <ais-search-box></ais-search-box>
    <ais-results>
      <template slot-scope="{ result }">
        <div @click="selectChamp(result)">
          <ais-highlight :result="result" attribute-name="body"></ais-highlight>
          <img class="champion_square" :src="championPath(result.body)" alt="">
        </div>
      </template>
    </ais-results>
  </ais-index>
</template>

<script>
export default {
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
.ais-results
  display grid
  grid-template-columns repeat(15, 80px)
  grid-gap 1em

.ais-highlight
  display none

.champion_square
  max-width 100%
  // max-height 20px
</style>
