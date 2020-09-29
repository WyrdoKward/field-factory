<template>
  <div v-if="isActive" class="selectedLocationContainer" >
    <h1>{{this.loc.Title}}</h1>
    <p>{{this.loc.Description}}<p>
    <div v-if="!eventActive" class="actions" >
      <!--<div v-for="action in loc.Actions" >
        <h2>{{action}}</h2>
      </div>-->
      <button type="button" @click="explore(loc.IdLocation)" style="margin-bottom: 80px;">Explore</button>
    </div>
    <LocationEvent v-if="this.eventActive" :event="this.event" />
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
    loc : Object
  },
  components :{
    LocationEvent : () => import('@/components/map/LocationEvent.vue')
  },
  data() {
    return {
      //loc : null,
      isActive: false,
      eventActive : false,
      event : null
    }
  },
  created() {
    bus.$on("selectLocation", data => {
      console.log('EVENT : Entering selectLocation : data = '+data.Title);
      this.isActive = true;
      this.loc = data;
    });
  },
  methods:{
    explore(locId){
        this.getEventFirstStep(locId);
      },
    getEventFirstStep(locId){ //return que le step 0
      console.log('getEvent('+locId );
        const json = JSON.stringify({IdFollower: 'Gustav',  IdLocation: locId});
        const options = {headers: {'Content-Type': 'application/json'}};
      axios
      .post('http://localhost:8080/api/Explore', json, options)
      .then(res => {
        this.event = res.data;
        this.eventActive = true;
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
