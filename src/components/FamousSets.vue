<template>
  <div class="famous_sets">
    <h1>Famous rune sets</h1>
    <div class="runeset" v-for="n in 10">
      <div class="winrate">52%</div>
      <div class="rune">A</div>
      <div class="rune">A</div>
      <div class="rune">A</div>
      <div class="rune">A</div>
      <div class="rune">A</div>
      <div class="rune">A</div>
    </div>
  </div>
</template>

<style lang="stylus">
.famous_sets
  display flex
  flex-direction column

.runeset
  display flex
</style>
