<template>

    <div class="wrapper " v-bind:class="{active: isActive}" @click="revealTile()" >

      <div class="tile" v-if="isActive" 
      :style="{ backgroundImage: 'url(assets/map/tiles/' + hex.img + '.png)' }">
        <Location v-if="hex.location" :loc="hex.location"/>
      </div>
      <div class="mask" v-else>
      </div>
    </div>
    
    <!--  <img class="tile" alt="tile" 
        :src="require(`@/assets/map/${imgPath}.png`)"
        @click="revealTile()"/>-->
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
        //imgPath: "http://1.bp.blogspot.com/-8PfnHfgrH4I/TylX2v8pTMI/AAAAAAAAJJ4/TICBoSEI57o/s1600/search_by_image_image.png"
        url : "http://localhost:8080/"
               
      }
      
    },
    methods: {
      revealTile() {
        //this.imgPath = this.hex.img
        this.isActive = true;
        //alert(this.isActive);
      }
    }
  }
</script>

<style lang="scss">

$baseUrl: 'http://localhost:8080/';

.wrapper{
  display: inline-block;
  height: fit-content;
  width: fit-content;
}

.wrapper:hover{
  //box-shadow: 4px 4px 8px #aaa;
}

.tile, .mask{
  height: 198px;
  width: 174px;
  margin: -30px -2px;
  display: inline-block;
  background-position: center;
  //background-image: var(--bgTile);
  //background-image: url(@/assets/map/tileDef.png);
  //background-image: url($baseUrl+ 'assets/map/tiles/tileDef.png');
}

.wrapper > .mask{
  //background-image: url('assets/map/tiles/tileDef.png');
  background-image: url(http://localhost:8080/assets/map/tiles/tileDef.png);
}

.active{
  text-align: center;
}
</style>
