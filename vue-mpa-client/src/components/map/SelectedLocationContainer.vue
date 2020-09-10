<template>
  <div v-if="isActive" class="selectedLocationContainer" >
    <h1>{{loc.Title}}</h1>
    <p>{{loc.Description}}<p>
    <div class="actions" >
      <!--<div v-for="action in loc.Actions" >
        <h2>{{action}}</h2>
      </div>-->
      <button type="button" @click="explore(loc.Id)" style="margin-bottom: 80px;">Explore</button>
    </div>
    <LocationEvent v-if="eventActive" :event="this.event" />
  </div>
  <div v-else class="selectedLocationContainer empty"></div> 
</template>

<script>
import { bus } from "../main";
export default {
  name: "SelectedLocationContainer",
  props: {
    loc : Object,
  },
  
  data() {
    return {
      isActive: false,
      eventActive : false,
      event : null
    }
  },
  created() {
    bus.$on("selectLocation", data => {
      this.isActive = true;
      this.loc = data;
    });
  },
  methods:{
    explore(locId){
        // GET /location/{locId}/event
        //console.log('Getting a random event for location '+locId );
        this.event = getEvent(locId);
        eventActive = true;
      },
    getEvent(locId){ //return que le step 1
      return "{}";
    }
  },
};
</script>

</style scoped>
.selectedLocationContainer{
 float:right;
 width: 20%;
 height:400px;
}

.empty{
  //bgimage
}
</style>
