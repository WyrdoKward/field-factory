<template>

    <div class="wrapper " v-bind:class="{active: isActive}" @click="revealTile()" >

      <div class="tile" v-if="isActive" 
      :style="{ backgroundImage: 'url(assets/map/tiles/' + hex.LandType + '.png)' }">
        <Location v-if="hex.IdLocation" :locId="hex.IdLocation"/>
      </div>
      <div class="mask" v-else>
      </div>
    </div>
</template>

<script>

  export default {
    name: 'Tile',
    components: {
      Location: () => import('@/components/map/Location.vue')
    },
    props: {
      imgName: String,
      hex : Object,
    },
    data() {
      return {
        isActive: true, //à true pour le debug
      }
      
    },
    created: function () {
      if(this.hex.LandType.indexOf('ocean') > 0){
        setInterval(() => {
          this.refreshOceanTile();
        }, 5000);
      }
    },
    methods: {
      revealTile() {
        this.isActive = true;
      },
      refreshOceanTile(){
        let rnd = Math.floor(Math.random() * Math.floor(10));
        this.hex.LandType = this.radical+rnd;
      }
    },
    computed :{
      radical : function(){ 
        return this.hex.LandType.slice(0, this.hex.LandType.length-1);
      }
    }
  }
</script>

<style lang="scss">

.wrapper{
  display: inline-block;
  height: fit-content;
  width: fit-content;
}

.tile, .mask{
  height: 198px;
  width: 174px;
  margin: -32px -8px;
  display: inline-block;
  background-position: center;
}

.wrapper > .mask{
  background-image: url('/assets/map/tiles/tileDef.png');
}

.active{
  text-align: center;
}
</style>
