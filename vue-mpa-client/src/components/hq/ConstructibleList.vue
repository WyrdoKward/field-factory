<template>
    <div class="constructibleList" >
        <h1>Available furnitures</h1>
    </div>
</template>

<script>
  export default {
    name: 'ConstructibleList',
    components: {
      //Sub: () => import('@/components/HelloWorld.vue')
    },
    props: {
      //message: String,
    },
    data() {
      return {
        furnitureList: {},               
      }
    },
    created() {
        //fetch GET /furniture/available
        this.fetchAvailableFurnitures();
    },
    computed:{
      ...mapGetters(['getAvailableFurnitures']),
      FurnitureList : function(){
        return this.getAvailableFurnitures;
      }
    },
    methods: {
      ...mapActions(['fetchAvailableFurnitures']),
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
