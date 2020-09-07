<template>
  <div id="app">
    <h1>World</h1>
    <TilesContainer/>
    <a href="home.html">Home</a>
    <a href="test.html">Test</a>
    <svg>
      <defs>
        <clipPath id="clip_mine">
          <polygon 
            points="50,0  61,35 98,35
                    68,57 79,91 50,70
                    21,91 32,57 2,35 39,35"/>
        </clipPath>
      </defs>
    </svg>
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
$baseUrl: 'http://localhost:8080/';
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}

</style>
