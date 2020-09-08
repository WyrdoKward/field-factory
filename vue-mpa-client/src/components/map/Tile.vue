<template>

    <div class="wrapper " v-bind:class="{active: isActive}" @click="revealTile()" >

      <div class="tile" v-if="isActive" 
      :style="{ backgroundImage: 'url(assets/map/tiles/' + hex.LandType + '.png)' }">
        <Location v-if="hex.Location" :loc="hex.Location"/>
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
        isActive: false,
      }
      
    },
    methods: {
      revealTile() {
        this.isActive = true;
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
