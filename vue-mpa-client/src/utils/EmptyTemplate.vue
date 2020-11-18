<template>
    <div class="emptyTemplate" >
        <Sub :msg="Yoyo"/>
    </div>
</template>

<script>
  import { store, mapState, mapActions, mapGetters } from 'vuex'

  export default {
    name: 'EmptyTemplate',
    components: {
      Sub: () => import('@/components/HelloWorld.vue')
    },
    props: {
      message: String,
    },
    created() {
        //fetch GET /furniture/possessed
    },
    computed:{
      ...mapGetters(['getterDuModule']),
      Field : function(){
        return this.getterDuModule;
      }
    },
    data() {
      return {
        outputVar: "Je suis utilisable dans tout le fichier avec this.outputVar",               
      }
    },
    methods: {
      myMethod() {
        return 'pouet'
      }
    }
  }
</script>

<style lang="scss">
</style>
