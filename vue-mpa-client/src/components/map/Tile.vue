<template>
    <div class="tile">
        <img alt="tile" 
        :src="require(`@/assets/map/${imgPath}.png`)"
        @click="revealTile()"/>
    </div>
</template>

<script>
export default {
  name: 'Tile',
  props: {
    imgName: String
  },
  data() {
    return {
      imgPath: 'tileDef'
      //imgPath: 'tile'+this.imgName
    }
  },
  methods: {
    revealTile() {
      this.imgPath = 'tile'+this.imgName
    }
  }
}
</script>

<style lang="scss">
.tile{
  width: fit-content;
  display: inline-block;
}
</style>
