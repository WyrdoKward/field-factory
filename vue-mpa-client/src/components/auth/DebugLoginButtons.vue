<template>
  <div>
    <div class="debugLoginButtons" >
        <h1 v-if="this.player">Welcome back {{this.player.IdPlayer}} ! </h1>
        <h1 v-else>Not logged</h1>
        <button type="button" @click="Login('wyrdokward', '123456Az')" style="margin-bottom: 80px;">Login as "wyrdokward"</button>
        <button type="button" @click="Login('nono', 'pwd')" style="margin-bottom: 80px;">Login as "nono"</button>
    </div>
    <div>
      <a href="/world">WORLD</a><br />
      <a href="/headquarters">Headquarters</a>
    </div>
  </div>
</template>

<script>
const axios = require('axios');

  export default {
    name: 'DebugLoginButtons',
    data() {
      return {
        player: null             
      }
    },
    created() {
      // vérifier le cookie ffauth et envoyer le token au serveur pour récupérer la session
      // /GET /api/auth/session
      axios
      .get('http://localhost:8080/api/auth/session')
      .then(res => {
        this.player = res.data;
        console.log('SUCCES');
        console.log(this.player);
      }).catch(err => {
        console.log('FAIL');
        console.log(err.response);
      });
    },
    methods: {
      Login(idPlayer, pwd) {
        const json = JSON.stringify({IdPlayer: idPlayer,  Mdp: pwd});
        const options = {headers: {'Content-Type': 'application/json'}};
        axios
      .post('http://localhost:8080/api/auth/login', json, options)
      .then(res => {
        this.player = res.data;
        console.log('SUCCES');
        console.log(this.player);
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
.debugLoginButtons > h1{
  float: left;
}
</style>
