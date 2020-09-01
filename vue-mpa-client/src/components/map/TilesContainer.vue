<template>
    <div class="tilesContainer">
      <h1>La carte du monde</h1>
      <Tile :imgName="0"/>
      <Tile :imgName="1"/>
      <Tile :imgName="0"/>
      <Tile :imgName="1"/>
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
      img0: 'tile0'
    }
  }
}
</script>

<style>
  .tilesContainer{
    width:fit-content;
    margin:auto;
  }
</style>
