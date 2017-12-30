<template>
<div class="select_rune">
  <h1>Select rune</h1>
  <div class="runes_container">
    <!-- Primary Path -->
    <div class="rune_path">
      <div class="rune_path_slot" @click="currentChoice = 'path'">
        <img :src="runePath(primaryPath)" v-if="primaryPath" alt="rune.name" class="rune_image">
      </div>
      <div class="slot_options" v-if="currentChoice === 'path'">
        <div class="rune" v-for="rune in runes" @click="selectPrimaryPath(rune)" :key="rune.id">
          <img :src="runePath(rune)" alt="rune.name" class="rune_image">
          {{ rune.name }}
        </div>
      </div>
      <span class="rune_text" v-else>Choose your primary path</span>
    </div>
    <template v-for="(n, index) in 4">
      <!-- Keystone, Greater Rune, Rune I, Rune II -->
      <div class="rune_path" :key="'rune_' + n">
        <div class="rune_path_slot" @click="currentChoice = index">
            <img :src="runePath({id:runeSet[index]})" v-if="runeSet[index]" alt="rune.name" class="rune_image">
        </div>
        <div class="slot_options" v-if="currentChoice === index">
          <div class="rune" v-for="rune in primaryPath.slots[index].runes" @click="selectRune(rune, index)" :key="rune.id">
            <img :src="runePath(rune)" alt="rune.name"  class="rune_image">
            {{ rune.name }}
          </div>
        </div>
        <span class="rune_text" v-else>Choose your {{ runeText[index] }}</span>
      </div>
    </template>

    <!-- Secondary Path -->
    <div class="rune_path">
      <div class="rune_path_slot" @click="currentChoice = 'path2'">
        <img :src="runePath(secondaryPath)" v-if="secondaryPath" alt="rune.name" class="rune_image">  
      </div>
      <div class="slot_options" v-if="currentChoice === 'path2'">
        <div class="rune" v-for="rune in runes" @click="selectSecondaryPath(rune)" :key="rune.id">
          <img :src="runePath(rune)" alt="rune.name" class="rune_image">
          {{ rune.name }}
        </div>
      </div>
      <span class="rune_text" v-else>Choose your secondary path</span>
    </div>

    <template v-for="(n, index) in 2">
      <!-- Rune I, Rune II-->
      <div class="rune_path" :key="'rune_' + (n + 4)">
        <div class="rune_path_slot" @click="currentChoice = index + 4">
          <img :src="runePath({id:runeSet[index + 4]})" v-if="runeSet[index + 4]" alt="rune.name" class="rune_image">
        </div>
        <div class="slot_options" v-if="currentChoice === index + 4">
          <div class="rune" v-for="rune in secondaryPath.slots[index + 2].runes" @click="selectRune(rune, index + 4)" :key="rune.id">
            <img :src="runePath(rune)" alt="rune.name" class="rune_image">
            {{ rune.name }}
          </div>
        </div>
        <span class="rune_text" v-else>Choose your {{ runeText[index + 4] }}</span>
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
      currentChoice: null,
      primaryPath: null,
      secondaryPath: null,
      runeSet: [undefined, undefined, undefined, undefined, undefined, undefined],
      runeText: ['keystone', 'greater rune', 'primary rune I', 'primary rune II', 'secondary rune I', 'secondary rune II'],
    }
  },
  methods: {
    selectPrimaryPath(rune) {
      this.primaryPath = rune;
      this.currentChoice = null;

      // On runePath change, clean already selected runes
      for (let i = 0; i < 4; i++) {
        this.runeSet[i] = undefined;
      }
    },
    selectSecondaryPath(rune) {
      this.secondaryPath = rune;
      this.currentChoice = null;    

      // On runePath change, clean already selected runes
      for (let i = 4; i < 6; i++) {
        this.runeSet[i] = undefined;
      }
    },
    selectRune(rune, index) {
      this.runeSet[index] = rune.id;
      this.currentChoice = null;

      // When all runes are selected, emit the runeIdentifier to the parent component
      if (this.isRuneSetFull()) this.$emit('runeSetSelected', this.runeSet.join(''));
    },
    runePath(rune) {
      return require(`../assets/runes/${rune.id}.png`);
    },
    isRuneSetFull() {
      return this.runeSet.every(elem => elem !== undefined);
    },
  },
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

.slot_options
  display flex

.slot_options .rune
  cursor:pointer;

.rune_slot_options
  display flex

.rune_path
  display flex
  align-items center

.rune_path_slot
  background black
  border-radius 100%
  height 42px
  width 42px
  cursor: pointer

.runes_container
  display grid
  grid-template-rows repeat(5, 1fr)
  grid-template-columns repeat(2, 1fr)
  grid-auto-flow column

.rune_image
  max-width 42px
  max-height 42px
</style>

