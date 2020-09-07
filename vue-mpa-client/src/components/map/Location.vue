<template>    
    <img class="location" alt="location" 
        :src="require(`@/assets/map/locations/${loc.img}.png`)"
        @click="showLocationDialog()"/>
</template>

<script>


  export default {
    name: 'Location',
    props: {
      loc : Object
    },
    data() {
      return {
          
      }
      
    },
    methods: {
      showLocationDialog() {
        alert('Is there a quest here ?');
      }
    }
  }
</script>

<style lang="scss">
.location{
    overflow: hidden;
    height: 198px;
    width: 174px;
    margin: auto;
    float: left;
    //background-image: var(--bgTile);
    //background-image: url(@/assets/map/tileDef.png);
}

//Effet de déplacement du marqueur (bounce ?)
.tile:hover{
  box-shadow: 4px 4px 8px #aaa;
}
</style>
