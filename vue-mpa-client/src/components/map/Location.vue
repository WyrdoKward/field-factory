<template> 
    <Town v-if="locId=='townCenter'" :loc="locId" />   
    <canvas v-else class="location"
      :style="{ backgroundImage: 'url(assets/map/locations/' + locId + '.png)' }"
        @click="displayLocationInfos()"
        @mousehover="hoverEffect()">
    </canvas>
</template>

<script>

  import { bus } from "@/pages/World/main";
  const axios = require('axios');

  export default {
    name: 'Location',
    components: {
      Town: () => import('@/components/map/Town.vue')
    },
    props: {
      locId : String
    },
    data() {
      return {
        actions: [],
        location : ""
      }
      
    },
    methods: {
      hoverEffect(){

      },
      displayLocationInfos(){
        //Nouveau composant
        console.log('displayLocationInfos')
        axios
        .get('http://localhost:8080/api/location/'+this.locId+'')
        .then(res => {
          this.location = res.data;
          this.location.IdLocation = this.locId;
          console.log('SUCCES');
          console.log(res.data);
        bus.$emit("selectLocation", this.location);
        console.log(this.location.id +' : ' +this.location.Title+ '\r\n\r\n'+ this.location.Description + '\r\n\r\nYou can :\r\n' + this.location.Verbs);
      
        }).catch(err => {
          console.log('FAIL');
          console.log(err.response);
        });}
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
</style>
