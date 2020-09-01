<template>
  <div id="app">
    <h1>World</h1>
    <TilesContainer/>
  </div>
</template>

<script>
//import { TilesContainer } from "@components/map/TilesContainer.vue";



export default {
  name: 'World',
  components: {
    TilesContainer: () => import('@/components/map/TilesContainer.vue')
  }
}

</script>

<style lang="scss">
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
