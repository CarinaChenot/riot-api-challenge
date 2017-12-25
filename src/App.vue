<template>
  <div id="app">
    <router-view/>
    <Select />
    <FamousSets />
  </div>
</template>

<script>
import Select from '@/components/Select';
import FamousSets from '@/components/FamousSets';

export default {
  name: 'app',
  components : {
    Select,
    FamousSets,
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
