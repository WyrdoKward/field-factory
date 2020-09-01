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
  float: left;
}
</style>
