<template>
  <div id="app">
    <router-view/>
    <Select @change="val => {sets = val; selectedSets = [val[0]]}"/>
	<Graph :sets="sets" :selectedSets="selectedSets"/>
    <FamousSets /> <!-- :sets="sets" -->
  </div>
</template>

<script>
import Select from '@/components/Select';
import FamousSets from '@/components/FamousSets';
import Graph from '@/components/Graph';
    
export default {
  name: 'app',
  components : {
    Graph,
    Select,
    FamousSets,
  },
   data() {
    return {
	  sets: [],
	  selectedSets: []
    }
  }
}
</script>

<style lang="stylus">
#app
  font-family 'Avenir', Helvetica, Arial, sans-serif
  -webkit-font-smoothing antialiased
  -moz-osx-font-smoothing grayscale
  color #2c3e50

</style>
