<template>
<div>
  <div v-if="isLocationLoaded" class="selectedLocationContainer" >
  debug = {{this.getLastSelectedLocation}}
    <h1>{{Location.Title}}</h1>
    <p>{{Location.Description}}<p>
      
 
    <div v-if="!isExploreActive" class="actions" >
      <!--<div v-for="action in location.Actions" >
        <h2>{{action}}</h2>
      </div>-->
      <button type="button" @click="exploreLocation()" style="margin-bottom: 80px;">Explore</button>
    </div>
    <LocationExplore v-if="this.isExploreActive" :explore="Explore"/>
  </div>
  <div v-else class="selectedLocationContainer empty"></div> 
</div>
</template>

<script>


  import { store, mapState, mapActions, mapGetters } from 'vuex'
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
      explore : null
    }
  },
  created() {
    bus.$on("selectLocation", data => {
      console.log('Entering selectLocationContainer : '+data.Location.Title);
      this.isExploreActive = false; // Réinitialise le div actions au changement de location
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
    computed:{
      ...mapGetters(['getLastSelectedLocation']),
      isLocationLoaded : function(){
        return (typeof(this.getLastSelectedLocation) !== 'undefined' && this.getLastSelectedLocation !== null)
      },
      isExploreActive : function(){
        return this.getLastSelectedLocation.Explore !== null
      },
      Location : function(){
        return this.getLastSelectedLocation.Location;
      },
      Explore : function(){
        return this.getLastSelectedLocation.Explore;
      }    
    },
  methods:{
    exploreLocation(){ 
      console.log('getEvent('+this.idLocation );
        const json = JSON.stringify({IdFollower: 'Gustav',  IdLocation: this.idLocation});
        const options = {headers: {'Content-Type': 'application/json'}};
      axios
      .post('http://localhost:8080/api/Explore', json, options)
      .then(res => {
        this.explore = res.data;
        //this.explore = mock[this.idLocation];
        this.isExploreActive = true;
        console.log('PostExplore');
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
