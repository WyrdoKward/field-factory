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
    <LocationExplore v-if="this.isExploreActive" :explore="this.explore"/>
  </div>
  <div v-else class="selectedLocationContainer empty"></div> 
</template>

<script>
import POSTExploreMockdummyLocation from '@/utils/POSTExploreMockdummyLocation.json'
import POSTExploreMockeglingen from '@/utils/POSTExploreMockeglingen.json'
const mock = {}
mock['dummyLocation'] = POSTExploreMockdummyLocation;
mock['eglingen'] = POSTExploreMockeglingen;


import { bus } from "@/pages/World/main";
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
    exploreLocation(){ // TODO endpoint POST DOIT RENVOYER Explore au lieu de Step0
      console.log('getEvent('+this.idLocation );
        const json = JSON.stringify({IdFollower: 'Gustav',  IdLocation: this.idLocation});
        const options = {headers: {'Content-Type': 'application/json'}};
      axios
      .post('http://localhost:8080/api/Explore', json, options)
      .then(res => {
        //this.explore = res.data;
        this.explore = mock[this.idLocation];
        this.isExploreActive = true;
        console.log('SUCCES');
        console.log(res.data);
        console.log('BOUCHON :');
        console.log(this.explore);
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
 width: 35%;
 height:600px;
}

.empty{
  /*bgimage*/
}
</style>
