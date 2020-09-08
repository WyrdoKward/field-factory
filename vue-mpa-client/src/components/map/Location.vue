<template> 
    <Town v-if="loc.type=='townCenter'" :loc="loc" />   
    <canvas v-else class="location"
      :style="{ backgroundImage: 'url(assets/map/locations/' + loc.type + '.png)' }"
        @click="showLocationDialog()"
        @mousehover="hoverEffect()">
    </canvas>


        <!--:src="require(`@/assets/map/locations/${hoverPath}${loc.img}.png`)"-->

</template>

<script>


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
        this.actions = this.getLocationActions(this.loc.id);
        console.log(this.loc.id + ' - ' + this.actions);
        alert(this.loc.msg+ '\r\n\r\n'+ 'You can :\r\n' + this.actions);
        this.displayActionsWindow();
      },
      hoverEffect(){

      },
      getLocationActions(locId){
        // GET /location/getAction/{locId}
        //console.log('Getting actions for location '+locId );
        return ['Explore', 'Meditate'];
      },
      displayActionsWindow(){
        //Nouveau composant
        console.log('displayActionsWindow')
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
