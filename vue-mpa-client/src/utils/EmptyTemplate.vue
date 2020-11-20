<template>
    <div class="emptyTemplate" >
        <Sub :msg="Yoyo"/>
    </div>
</template>

<script>
  export default {
    name: 'EmptyTemplate',
    components: {
      Sub: () => import('@/components/HelloWorld.vue')
    },
    props: {
      message: String,
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
