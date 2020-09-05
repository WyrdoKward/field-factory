<template>

    <div class="tile" @click="revealTile()" 
    :style="{ backgroundImage: 'url(assets/map/' + imgPath + '.png)' }">
    </div>
    
      <!--<img class="tile" alt="tile" 
        :src="require(`@/assets/map/${imgPath}.png`)"
        @click="revealTile()"/>-->
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
        //imgPath: "http://1.bp.blogspot.com/-8PfnHfgrH4I/TylX2v8pTMI/AAAAAAAAJJ4/TICBoSEI57o/s1600/search_by_image_image.png"
               
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
  height: 198px;
  width: 174px;
  margin: -30px -2px;
  display: inline-block;
  background-position: center;
  //background-image: var(--bgTile);
  //background-image: url(@/assets/map/tileDef.png);
}

.tile:hover{
  box-shadow: 4px 4px 8px #aaa;
}
</style>
