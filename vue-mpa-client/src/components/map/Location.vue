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
  import { store, mapState, mapActions } from 'vuex'
  //import { getLocationWithActions } from "@/shared/axiosCalls";
  import locationModule from "@/api/stores/location.module";


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
        //locationWithActions : ""
      }
      
    },
    created() { 
      //this.getLocationWithActions = axiosCalls.getLocationWithActions;
      console.log('0')
    },
    computed:{
      ...mapState(['locationWithActions']),
    },
    methods: {
      ...mapActions(['fetchLocationWithActions', 'foo']),
      displayLocationInfos(){ //https://haxzie.com/architecting-http-clients-vue-js-network-layer !!!!!
        console.log('1') //https://stackoverflow.com/questions/52092873/how-do-i-make-axios-api-call-in-a-separate-component-file
        //this.fetchLocationWithActions(this.locId);
        this.foo();

        console.log('5 - axios called :');
        console.log(this.locationWithActions);
        bus.$emit("selectLocation", this.locationWithActions);
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
