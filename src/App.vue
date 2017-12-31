<template>
<div id="app">
    <div id="header">
        <span>Runes playstyle
            <em>built for winter '17 API challenge</em>
    </span>
    </div>

    <div class="md-layout md-alignment-top-space-around">
        <div class="box md-layout-item md-size-25">
            <Select v-on:runeSetsLoaded="val => {sets = val;}" 
                    v-on:runeSetSelected="val => {selectedSets = [val];}" />
        </div>        
        <div class="box md-layout-item md-size-35">
            <Graph :sets="sets" :selectedSets="selectedSets"/>
        </div>
        <div class="box md-layout-item md-size-30">
            <FamousSets :sets="sets" v-on:runeSetSelected="val => {selectedSets = [val];}"/>
        </div>
    </div>
</div>
</template>

<script>
    import Select from '@/components/Select';
    import FamousSets from '@/components/FamousSets';
    import Graph from '@/components/Graph';
    import VueMaterial from 'vue-material'    
    import 'vue-material/dist/vue-material.min.css'
    import 'vue-material/dist/theme/default-dark.css'

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
@import url('https://fonts.googleapis.com/css?family=Acme')
@font-face {
    font-family: 'friz quadrata';
    src: url(/static/friz-quadrata.ttf);
}



body
  margin:0
  padding:0
  background:url('/static/background.jpg') #857BAD
  background-size:cover

#app
  //font-family 'Acme', 'Avenir', Helvetica, Arial, sans-serif
  -webkit-font-smoothing antialiased
  -moz-osx-font-smoothing grayscale
  color #2c3e50


#header
  height:95px
  background:rgba(18,8,59,.5)
  box-shadow: 0 4px 2px -2px #261958;
  margin-bottom: 20px
  
#header span
  display:block
  font-family: 'friz quadrata'
  font-weight:bold;
  padding: 32px 0 0 35px
  font-size:45px
  color: #FBDEA8
  text-shadow: -1px 0 #12083B, 0 1px #12083B, 1px 0 #12083B, 0 -1px #12083B

#header span em
  font-size:30%
  position:relative
  top:10px





/* moving to box-sizing (always) */


* { box-sizing: border-box; }

/* styles du conteneur */
/* hozizontal display and wrap enabled */
.container-grid-4 {
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
}
/* direct children's styles */
/* width defined to 25% */
.container-grid-4 > * {
    flex: 0 0 auto;
    width: 25%;
}

.box
  background:rgba(18,8,59,.5)
  color:gainsboro
  box-shadow: 4px 4px 2px -2px #261958;
  height:500px
  overflow:hidden



</style>
