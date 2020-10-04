<template> 
  <div class="locationContainer" :title="locId">
    <Town v-if="locId=='townCenter'" :loc="locId" />   
    <canvas v-else class="location"
      :style="{ backgroundImage: 'url(assets/map/locations/' + locId + '.png)' }"
        @click="displayLocationInfos()">
    </canvas>
  </div>
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
        locationWithActions : ""
      }
      
    },
    methods: {
      displayLocationInfos(){
        //Nouveau composant
        console.log('displayLocationInfos')
        axios
        .get('http://localhost:8080/api/location/'+this.locId+'/withactions')
        .then(res => {
          this.locationWithActions = res.data;
          this.locationWithActions.IdLocation = this.locId;
          console.log('SUCCES');
          console.log(res.data);
          bus.$emit("selectLocation", this.locationWithActions);
          console.log(this.locationWithActions.Location.Id +' : ' +this.locationWithActions.Location.Title+ '\r\n\r\n'+ this.locationWithActions.Location.Description + '\r\n\r\nYou can :\r\n' + this.locationWithActions.Location.Verbs);
      
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
