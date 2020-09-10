<template> 
    <Town v-if="loc.LocationType=='townCenter'" :loc="loc" />   
    <canvas v-else class="location"
      :style="{ backgroundImage: 'url(assets/map/locations/' + loc.LocationType + '.png)' }"
        @click="showLocationDialog()"
        @mousehover="hoverEffect()">
    </canvas>
</template>

<script>

  import { bus } from "@/pages/World/main";

  export default {
    name: 'Location',
    components: {
      Town: () => import('@/components/map/Town.vue')
    },
    props: {
      loc : Object
    },
    data() {
      return {
        actions: []
      }
      
    },
    methods: {
      showLocationDialog() {
        //Vérif coté serveur pour savoir quelles actions en cours
        this.actions = this.getLocationActions(this.loc.Id);
        console.log(this.loc.id +' : ' +this.loc.Title+ '\r\n\r\n'+ this.loc.Description + '\r\n\r\nYou can :\r\n' + this.actions);
        this.displayActionsWindow();
      },
      hoverEffect(){

      },
      getLocationActions(locId){
        // GET /location/getAction/{locId}
        //console.log('Getting actions for location '+locId );
        return this.loc.Verbs;
      },
      displayActionsWindow(){
        //Nouveau composant
        console.log('displayActionsWindow')
        
        bus.$emit("selectLocation", this.loc);
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
</style>
