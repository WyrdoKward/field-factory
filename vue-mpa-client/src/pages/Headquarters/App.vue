<template>
  <div id="app">
      <DebugLoginButtons/>
      <Building :buildingType="oldBookStore"/>
      <ConstructibleList />
  </div>
</template>

<script>

export default {
  name: 'Headquarters',
  components: {
    DebugLoginButtons: () => import('@/components/auth/DebugLoginButtons.vue'),
    Building: () => import('@/components/hq/Building.vue'),
    ConstructibleList: () => import('@/components/hq/ConstructibleList.vue')
  }
}

</script>

<style lang="scss">
$baseUrl: 'http://localhost:8080/';
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

footer{
  margin: 10px;
}

</style>
