<template>

    <!--<div class="tile" @click="revealTile()" 
    v-bind:style="tileStyle">
    </div>-->
    
      <img class="tile" alt="tile" 
        :src="require(`@/assets/map/${imgPath}.png`)"
        @click="revealTile()"/>
</template>

<script>


  export default {
    name: 'Tile',
    props: {
      imgName: String
    },
    data() {
      return {
        imgPath: 'tileDef',
        //imgPath: 'tile'+this.imgName
        tileStyle:  {
          //background: require(`url('@/assets/map/${imgPath}.png')`)
        }
        
      }
      
    },
    methods: {
      revealTile() {
        this.imgPath = this.imgName
      }
    }
  }
</script>

<style lang="scss">
.tile{
  /*height: 198px;
  width: 174px;*/
  margin: -35px -20px;
  display: inline-block;
  //background-image: var(--bgTile);
  //background-image: url(@/assets/map/tileDef.png);
}

.tile:hover{
  box-shadow: 4px 4px 8px #aaa;
}
</style>
