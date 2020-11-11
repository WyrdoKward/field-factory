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
  import { store, mapState, mapActions, mapGetters } from 'vuex'


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
      }
    },
    computed:{
      ...mapState(['locationWithActions']),
      ...mapGetters(['getLocationWithActions'])    
    },
    methods: {
      ...mapActions(['fetchLocationWithActions', 'fetchSelectedExploration', 'foo']),
      displayLocationInfos(){
        this.fetchLocationWithActions(this.locId);
        this.fetchSelectedExploration(this.locId); // récup l'explore juste après avoir fait le get location?
        //bus.$emit("selectLocation", this.locationWithActions);
      }
    }
  }
</script>

<style lang="scss">
.location{
    overflow: hidden;
    height: 100px;
    width: 150px;
    padding: 49px 12px;
    float: left;
    background-position-x: -20px;
    background-position-y: 10px;

}

.location:hover{
  background-position-x: 190px;
  cursor: pointer;
}
</style>
