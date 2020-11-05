<template>
  <div id="app">
    <h1>Hello Test</h1>
    <button @click="callFoo" >Foo</button>
    <button @click="callFooParam" >FooParam</button>
    <h2>{{text}}</h2>
  </div>
</template>

<script>

//STORE

import { mapActions } from "vuex";

export default {
  name: 'Test',
  data(){
    return{
      text : "change me"
    }
  },
  methods:{
    ...mapActions(["fooParam", "foo"]),
    callFooParam() {
        this.fooParam({
          param: 'paramètre'
        })
    },
    callFoo(){
      this.foo();
    }
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
