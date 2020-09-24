<template>
    <div class="debugLoginButtons" >
        <h1 v-if="this.player">Logged in as : {{this.player.Idplayer}}</h1>
        <h1 v-else>Not logged</h1>
        <button type="button" @click="Login("wyrdokward", "123456")" style="margin-bottom: 80px;">Login as "wyrdokward"</button>
        <button type="button" @click="Login("nono", "pwd")" style="margin-bottom: 80px;">Login as "nono"</button>
    </div>
</template>

<script>
const axios = require('axios');

  export default {
    name: 'DebugLoginButtons',
    data() {
      return {
        player = null;             
      }
    },
    methods: {
      Login(idPlayer, pwd) {
        axios
      .post('http://localhost:8080/api/auth/', {idPlayer: idPlayer,  password: pwd})
      .then(res => {
        this.player = res.data;
        console.log('SUCCES');
        console.log(res.data);
      }).catch(err => {
        console.log('FAIL');
        console.log(err.response);
      });
      }
    }
  }
  
  //Ajouter un event pour envoyer this.player aux autres composants ?
</script>

<style lang="scss">
</style>
