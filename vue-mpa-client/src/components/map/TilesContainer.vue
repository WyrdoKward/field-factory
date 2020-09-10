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
      hexes : [[{"Id":0,"X":0,"Y":0,"LandType":"tile_forest_conifer_dense_covered_green_8","Location":null},{"Id":1,"X":0,"Y":1,"LandType":"tile_hills_dense_covered_green_5","Location":null},{"Id":2,"X":0,"Y":2,"LandType":"tile_grassland_sparse_clear_green_3","Location":{"Id":1,"Title":"Otrock","Description":"This is my hometown.","LocationType":"townCenter","Verbs":["Parler","Piller","Construire"]}},{"Id":3,"X":0,"Y":3,"LandType":"tile_grassland_sparse_clear_green_4","Location":null},{"Id":4,"X":0,"Y":4,"LandType":"tile_forest_deciduous_dense_covered_green_0","Location":null}],[{"Id":5,"X":1,"Y":0,"LandType":"tile_forest_conifer_dense_covered_green_1","Location":null},{"Id":6,"X":1,"Y":1,"LandType":"tile_hills_sparse_covered_green_4","Location":null},{"Id":7,"X":1,"Y":2,"LandType":"tile_hills_sparse_covered_green_5","Location":null},{"Id":8,"X":1,"Y":3,"LandType":"tile_grassland_sparse_covered_green_4","Location":null},{"Id":9,"X":1,"Y":4,"LandType":"tile_forest_deciduous_sparse_covered_green_6","Location":null}],[{"Id":10,"X":2,"Y":0,"LandType":"tile_ocean_waves_small_dark_4","Location":null},{"Id":11,"X":2,"Y":1,"LandType":"tile_ocean_waves_small_dark_7","Location":null},{"Id":12,"X":2,"Y":2,"LandType":"tile_grassland_dense_covered_green_9","Location":null},{"Id":13,"X":2,"Y":3,"LandType":"tile_forest_deciduous_sparse_clear_green_8","Location":{"Id":1,"Title":"Peaceful cave","Description":"The entrance is collapsed, but it can provide a decent shelter for a weary traveller.","LocationType":"mine","Verbs":["Explorer","Méditer"]}},{"Id":14,"X":2,"Y":4,"LandType":"tile_forest_conifer_sparse_covered_green_7","Location":null}],[{"Id":15,"X":3,"Y":0,"LandType":"tile_ocean_waves_big_dark_9","Location":null},{"Id":16,"X":3,"Y":1,"LandType":"tile_ocean_waves_small_dark_4","Location":null},{"Id":17,"X":3,"Y":2,"LandType":"tile_grassland_sparse_covered_green_4","Location":{"Id":1,"Title":"Mine","Description":"Maybe there could be some stuff in here.","LocationType":"mine","Verbs":["Explorer","Piller"]}},{"Id":18,"X":3,"Y":3,"LandType":"tile_hills_sparse_covered_green_4","Location":null},{"Id":19,"X":3,"Y":4,"LandType":"tile_ocean_waves_small_dark_3","Location":null}],[{"Id":20,"X":4,"Y":0,"LandType":"tile_ocean_waves_small_dark_6","Location":null},{"Id":21,"X":4,"Y":1,"LandType":"tile_ocean_waves_small_dark_0","Location":null},{"Id":22,"X":4,"Y":2,"LandType":"tile_ocean_waves_small_dark_4","Location":null},{"Id":23,"X":4,"Y":3,"LandType":"tile_ocean_waves_small_dark_0","Location":null},{"Id":24,"X":4,"Y":4,"LandType":"tile_ocean_waves_big_dark_7","Location":null}]]
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
    float:left;
    width:80%;
  }
  
  .mapRow{
    height : fit-content;
    width:fit-content;
    margin:auto;
  }
  
  .mapRow:nth-child(even){
    padding-left:156px;
  }
</style>
