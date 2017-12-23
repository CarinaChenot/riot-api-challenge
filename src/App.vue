<template>
  <div id="app">
    <router-view/>
    <champion />
  </div>
</template>

<script>
import Champion from '@/components/Champion';

export default {
  name: 'app',
  components : {
    Champion,
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
