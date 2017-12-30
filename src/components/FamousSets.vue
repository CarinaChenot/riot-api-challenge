<template>
<div class="famous_sets">
    <h1>Famous rune sets</h1>
    <div @click="selectSet(set)" class="runeset" v-for="set in sets.sort((s1,s2) => s2.winRate -s1.winRate).slice(0, 5)">
        <div class="winrate">{{ (100 * set.winRate).toFixed(1) }}%</div>
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
                this.$emit('runeSetSelected', set._id.runeIdentifier);
            }
        },
        data() {
            return {
                sortBy: 'best/winrate',
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
</style>
