<template>
  <div id="app">
      <DebugLoginButtons/>
      <TilesContainer/>
      <SelectedLocationContainer />
  </div>
    <!--<footer>
      <a href="home.html">Home</a>
      <a href="test.html">Test</a>
    </footer>
    <svg>
      <defs>
        <clipPath id="clip_mine">
          <polygon 
            points="50,0  61,35 98,35
                    68,57 79,91 50,70
                    21,91 32,57 2,35 39,35"/>
        </clipPath>
      </defs>
    </svg>-->
</template>

<script>

export default {
  name: 'World',
  components: {
    TilesContainer: () => import('@/components/map/TilesContainer.vue'),
    SelectedLocationContainer: () => import('@/components/map/SelectedLocationContainer.vue'),
    DebugLoginButtons: () => import('@/components/auth/DebugLoginButtons.vue')
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
