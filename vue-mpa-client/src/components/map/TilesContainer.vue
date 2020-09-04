<template>
    <div id="tilesContainer">
      <h1>La carte du monde</h1>
      <Tile v-for="h in hex" :key="h.id" :imgName="h.img" />
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
      hex: [
      {
          x: 0,
          y: 0,
          img: '0',
          id:0
      },
      {
          x: 0,
          y: 1,
          img: '1',
          id:1
      },
      {
          x: 0,
          y: 2,
          img: '1',
          id:2
      },
      {
          x: 1,
          y: 0,
          img: '1',
          id:3
      },
      {
          x: 1,
          y: 1,
          img: '0',
          id:4
      },
      {
          x: 1,
          y: 2,
          img: '0',
          id:5
      },
    ]
    }
  }
}

/*new Vue({
  el: '#tilesContainer',
  data: {
    
  }
});*/
</script>

<style>
  #tilesContainer{
    width:fit-content;
    margin:auto;
  }
</style>
