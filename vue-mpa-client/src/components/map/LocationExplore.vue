<template>
  <div v-if="explore" class="locationEvent" >
    <h2>{{this.explore.Event.Title}}</h2>
    <div v-for="step in this.explore.Event.Steps" :key="step.Id" class="eventStep" >
      <h3>{{step.Text}}</h3>
      <ul v-if="step.Choices" class="choices">
          <li class="choice" v-for="choice in step.Choices" :key="choice.Id" :class="{isSelected: choice.IsSelected}">
              <span @click="processChoice(choice.Id)">{{choice.Text}}</span>
          </li>
      </ul>
    </div>
      <Timer :deadline="explore.DateNextStep" @onFinish="log()" />
  </div>
</template>

<script>
const axios = require('axios');

  export default {
    name: 'LocationEvent',
    props: {
      explore : Object
    },
    data() {
      return {
      }
    },
  components :{
    Timer : () => import('@/components/core/Timer.vue')
  },
    methods: {
      log(){
        console.log('booooo');
      },
      processChoice(choiceId) {
        console.log('Location: #'+this.explore.IdLocation);
        console.log('Choice: #'+choiceId);
        // PUT: api/Explore/
        // {"IdLocation": "dummyLocation", "IdChoice": 2}
        const json = JSON.stringify({IdLocation: this.explore.IdLocation,  IdChoice: choiceId});
        const options = {headers: {'Content-Type': 'application/json'}};
        axios
        .put('http://localhost:8080/api/Explore', json, options)
        .then(res => {
          this.explore = res.data;
          console.log('PutExplore');
          console.log(res.data);
        }).catch(err => {
          console.log('FAIL');
          console.log(err.response);
          alert('Etape pas finie ! (5 min)')
        });
      },
    }
  }
</script>

<style lang="scss">
.choice:hover, .choices > .isSelected{
  color: forestgreen;
}

.choice:hover{
  color: forestgreen;
  cursor: pointer;
}
</style>
