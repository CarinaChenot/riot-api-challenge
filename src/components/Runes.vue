<template>
<div class="select_rune">
  <h1>Select rune</h1>
  <div class="runes-grid">
    <div class="rune" v-for="rune in runes" @click="selectRune(rune)" :key="rune.id">
      <img :src="runePath(rune)" alt="rune.name">
      {{ rune.name }}
    </div>
  </div>
  <div class="runes_child" v-if="selectedRune">
    <template v-for="slot in selectedRune.slots">
      <div class="rune-slot">Slot</div>
      <div class="rune_slot_options">
          <div class="child-rune" v-for="childRune in slot.runes">
            <img :src="runePath(childRune)" :alt="childRune.name">
            {{ childRune.name }}
          </div>
      </div>
    </template>
  </div>
</div>
</template>

<script>
import runesReforged from '@/data/runesReforged.json';

export default {
  data() {
    return {
      runes: runesReforged,
      selectedRune: null,
    }
  },
  methods: {
    selectRune(rune) {
      this.selectedRune = rune;
      // console.log(this.selectedRune.slots);

    },
    runePath(rune) {
      // console.log(rune.id)
      return require(`../assets/runes/${rune.id}.png`);
    }
  }
}
</script>

<style lang="stylus">
.select_rune
  display flex
  flex-direction column
  align-items center

.runes-grid
  display grid
  grid-auto-flow column
  justify-items center
  grid-gap 1em
  max-width 50vw

.rune
  display flex
  flex-direction column
  align-items center

.runes_child
  display grid
  grid-template-columns repeat(2, 1fr)

.rune_slot_options
  display flex
</style>

