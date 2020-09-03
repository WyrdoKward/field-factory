<template>
    <div id="tilesContainer">
      <h1>La carte du monde</h1>
      <!--<Tile v-repeat="hex" :imgName={img} />-->
      <div v-for="h in hex" :key="h.img" >
         <Tile :imgName="h.img" />
      </div>

      <!--
        <template v-for="u in users">
          <user :user="u" :key="user.id"></user>
        </template>

<Tile :imgName="0"/>
      <Tile :imgName="1"/>
      <Tile :imgName="0"/>
      <Tile :imgName="1"/>-->
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

      },
      {
          x: 0,
          y: 1,
          img: '1',

      },
      {
          x: 0,
          y: 2,
          img: '1',

      },
      {
          x: 1,
          y: 0,
          img: '1',

      },
      {
          x: 1,
          y: 1,
          img: '0',

      },
      {
          x: 1,
          y: 2,
          img: '0,'

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
