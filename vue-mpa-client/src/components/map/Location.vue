<template>    
    <canvas class="location mine" alt="location" 
      :style="{ backgroundImage: 'url(assets/map/locations/' + loc.img + '.png)' }"
        @click="showLocationDialog()"
        @mousehover="hoverEffect()"/>


        <!--:src="require(`@/assets/map/locations/${hoverPath}${loc.img}.png`)"-->

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
      },
      hoverEffect(){

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
    background-position-x: -20px;
}

.location:hover{
  background-position-x: 190px;
}

//Effet de déplacement du marqueur (bounce ?)
.tile:hover{
  //box-shadow: 4px 4px 8px #aaa;
}

.mine{
  //clip-path: url(#clip_mine);
}

.mine:hover{
  //box-shadow: 10px 10px 2px #000000;

}
</style>
