<template>
    <div id="tilesContainer">
      <h1>La carte du monde</h1>
      <button type="button" v-on:click="saveFile()" style="margin-bottom: 80px;">Generate Map</button>
      <div class="mapRow" v-for="line in hexes">
        <Tile v-for="h in line" :key="h.id" :hex="h" />
      </div>
    </div>
</template>

<script>
// Hex Map
/* import { defineGrid } from 'honeycomb-grid'

const Grid = defineGrid()
const grid1 = Grid.rectangle({ width: 4, height: 4 });

grid1.forEach(hex => {
  PrintTile(hex.toCartesian)
  }) */

export default {
  name: 'TilesContainer',
  components: {
    Tile: () => import('@/components/map/Tile.vue')
  },
  data () {
    return {
      img0: 'tile0',
      hexes: [[
      {
          x: 0,
          y: 0,
          img: 'grass0',
          id:0,
          location:{
            id:1,
            type:'mine',
            msg:'Is there a quest here ?',
            //actions:['Explore', 'Meditate']
          }
      },
      {
          x: 0,
          y: 1,
          img: 'hills0',
          id:1
      },
      {
          x: 0,
          y: 2,
          img: 'hills1',
          id:2
      }],
      [
      {
          x: 1,
          y: 0,
          img: 'grass1',
          id:3
      },
      {
          x: 1,
          y: 1,
          img: 'forest0',
          id:4,
          location:{
            id:2,
            type:'mine',
            msg:'There is something to explore in this forest.',
            //actions:['Explore', 'Meditate']
          }
      },
      {
          x: 1,
          y: 2,
          img: 'forest1',
          id:5
      }],
      [
      {
          x: 2,
          y: 0,
          img: 'hills0',
          id:6
      },
      {
          x: 2,
          y: 1,
          img: 'forest1',
          id:7
      },
      {
          x: 2,
          y: 2,
          img: 'ocean0',
          id:8
      }],
      [
      {
          x: 2,
          y: 0,
          img: 'hills0',
          id:6
      },
      {
          x: 2,
          y: 1,
          img: 'ocean0',
          id:7
      },
      {
          x: 2,
          y: 2,
          img: 'ocean0',
          id:8
      }],
      [
      {
          x: 2,
          y: 0,
          img: 'forest1',
          id:6
      },
      {
          x: 2,
          y: 1,
          img: 'forest0',
          id:7
      },
      {
          x: 2,
          y: 2,
          img: 'hills0',
          id:8
      }]]
    }
  },
  methods: {
    generateHexes: function(){
      for (let i = 0; i < array.length; i++) {
        const element = array[i];
        
      }
    },
    saveFile: function() {
      const data = JSON.stringify(this.hexes)
      const blob = new Blob([data], {type: 'text/plain'})
      const e = document.createEvent('MouseEvents'),
      a = document.createElement('a');
      a.download = "map.json";
      a.href = window.URL.createObjectURL(blob);
      a.dataset.downloadurl = ['text/json', a.download, a.href].join(':');
      e.initEvent('click', true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);
      a.dispatchEvent(e);
    }
  }
}
</script>

<style>
  #tilesContainer{
    width:fit-content;
    margin:auto;
  }
  
  .mapRow{
    height : fit-content;
    width:fit-content;
    margin:auto;
  }
  
  .mapRow:nth-child(even){
    padding-left:167px;
  }
</style>
