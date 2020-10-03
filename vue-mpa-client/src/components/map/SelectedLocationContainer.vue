<template>
  <div v-if="this.isLocationActive" class="selectedLocationContainer" >
    <h1>{{this.location.Title}}</h1>
    <p>{{this.location.Description}}<p>
    <div v-if="!isExploreActive" class="actions" >
      <!--<div v-for="action in location.Actions" >
        <h2>{{action}}</h2>
      </div>-->
      <button type="button" @click="exploreLocation()" style="margin-bottom: 80px;">Explore</button>
    </div>
    <LocationExplore v-if="this.isExploreActive" :explore="this.explore" />
  </div>
  <div v-else class="selectedLocationContainer empty"></div> 
</template>

<script>
import { bus } from "@/pages/World/main";
import json from '@/utils/dummyLocationEventStep0.json'
const axios = require('axios');

export default {
  name: "SelectedLocationContainer",
  props: {
  },
  components :{
    LocationExplore : () => import('@/components/map/LocationExplore.vue')
  },
  data() {
    return {
      idLocation : null,
      isLocationActive: false,
      location : null,
      isExploreActive : false,
      explore : null
    }
  },
  created() {
    bus.$on("selectLocation", data => {
      console.log('EVENT : Entering selectLocation : data = '+data.Location.Title);
      this.isLocationActive = true;
      this.location = data.Location;
      this.idLocation = data.IdLocation;
      console.log('idLocation ='+this.idLocation)
      if(data.Explore){        
        this.isExploreActive = true;
        this.explore = data.Explore;
      }
    });
  },
  methods:{
    exploreLocation(){
        this.getEventFirstStep(this.idLocation);
      },
    getEventFirstStep(locId){ //return que le step 0
      console.log('getEvent('+locId );
        const json = JSON.stringify({IdFollower: 'Gustav',  IdLocation: locId});
        const options = {headers: {'Content-Type': 'application/json'}};
      axios
      .post('http://localhost:8080/api/Explore', json, options)
      .then(res => {
        this.explore = res.data;
        this.isExploreActive = true;
        console.log('SUCCES');
        console.log(res.data);
      }).catch(err => {
        console.log('FAIL');
        console.log(err.response);
      });
    }
  },
};
</script>

<style scoped>

.selectedLocationContainer{
 float:right;
 width: 20%;
 height:600px;
}

.empty{
  /*bgimage*/
}
</style>
