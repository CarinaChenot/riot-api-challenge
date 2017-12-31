<template>
<div class="famous_sets">
    <h2>Most Famous</h2>
    <div @click="selectSet(set)" class="runeset" v-for="set in sortByWinrate(sets)" v-bind:class="{ active: selectedSet == set._id.runeIdentifier }">
        <div class="winrate">WR :<br/>{{ (100 * set.winRate).toFixed(1) }}%</div>
        <div class="usage">Used in:<br/>{{ (100 * set.p).toFixed(1) }}%</div>
        <div class="rune" v-for="n in 6">
            <img :src="runePath(set._id.runeIdentifier.substr(4*(n - 1), 4))" alt="rune.name" class="rune_image">
    </div>
    </div>
    <h2>Most used</h2>
    <div @click="selectSet(set)" class="runeset" v-for="set in sortByUsage(sets)" v-bind:class="{ active: selectedSet == set._id.runeIdentifier }">
        <div class="winrate">WR :<br/>{{ (100 * set.winRate).toFixed(1) }}%</div>
        <div class="usage">Used in:<br/>{{ (100 * set.p).toFixed(1) }}%</div>
        <div class="rune" v-for="n in 6">
            <img :src="runePath(set._id.runeIdentifier.substr(4*(n - 1), 4))" alt="rune.name" class="rune_image">
    </div>
    </div>
    </div>
</template>

<script>
    export default {
        props: [
            'sets',
            'champion',
        ],
        methods: {
            runePath(id) {
                return require(`../assets/runes/${id}.png`);
            },
            selectSet(set){
                this.selectedSet = set._id.runeIdentifier;
                this.$emit('runeSetSelected', set._id.runeIdentifier);
            },
            sortByWinrate(sets){
                var total = sets.reduce((t, s)=> t + s.count, 0);
                return sets
                    .map(function(s){ s['p'] = s.count / total; return s;})
                    .sort((s1,s2) => s2.winRate -s1.winRate).slice(0, 4)
            },
            sortByUsage(sets){
                var total = sets.reduce((t, s)=> t + s.count, 0);
                return sets
                    .map(function(s){ s['p'] = s.count / total; return s;})
                    .sort((s1,s2) => s2.count -s1.count).slice(0, 4)
            }
        },
        data() {
            return {
                sets: [],
                selectedSet: undefined
            }
        },
    }
</script>

<style lang="stylus">
.famous_sets
  display flex
  flex-direction column

.runeset
  display flex
  cursor pointer

.runeset.active
  background:yellow

.winrate
  color:orange
  margin:2px 5px

.usage
  color:#53e053
  margin:2px 5px
</style>
