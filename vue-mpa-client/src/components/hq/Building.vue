<template>
    <div class="buildingSide" >
        <h1>Your headquarters : {{this.buildingType}}</h1>
    </div>
</template>

<script>
  import { store, mapState, mapActions, mapGetters } from 'vuex'

  export default {
    name: 'Building',
    components: {
      //Sub: () => import('@/components/HelloWorld.vue')
    },
    props: {
      buildingType: String,
    },
    data() {
      return {              
      }
    },
    created() {
        //fetch GET /furniture/possessed
        this.fetchPossessedFurnitures();
    },
    computed:{
      ...mapGetters(['getPossessedFurnitures']),
      FurnitureList : function(){
        return this.getPossessedFurnitures;
      }
    },
    methods: {
      ...mapActions(['fetchPossessedFurnitures']),
      addNewFurniture(idFurniture) {
        //POST /provide/{idFurniture}
      },
      lvlUpFurniture() {
        //PUT /provide/{idFurniture} //le level est calculé coté server
      }
    }
  }
</script>

<style lang="scss">
</style>
