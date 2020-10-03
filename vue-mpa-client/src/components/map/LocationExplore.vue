<template>
  <div v-if="explore" class="locationEvent" >
    <h2>{{this.explore.Event.Title}}</h2>
    <div v-for="step in this.explore.Event.Steps" :key="step.Id" class="eventStep" >
      <h3>{{step.Text}}</h3>
      <ul v-if="step.Choices" class="choices">
          <li class="choice" v-for="choice in step.Choices" :key="choice.Id">
              <span @click="processChoice(choice.Id)">{{choice.Text}}</span>
          </li>
      </ul>
    </div>
  </div>
</template>

<script>
const axios = require('axios');

import step0 from '@/utils/dummyLocationEventStep0.json'
import step1 from '@/utils/dummyLocationEventStep1.json'
import step2 from '@/utils/dummyLocationEventStep2.json'
import step3 from '@/utils/dummyLocationEventStep3.json'
import step4 from '@/utils/dummyLocationEventStep4.json'
const steps = {}
steps[0] = step0;
steps[1] = step1;
steps[2] = step2;
steps[3] = step3;
steps[4] = step4;


  export default {
    name: 'LocationEvent',
    props: {
      explore : Object
    },
    data() {
      return {
        //explore : null,
      }
    },
    methods: {
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
          console.log('SUCCES');
          console.log(res.data);
        }).catch(err => {
          console.log('FAIL');
          console.log(err.response);
        });
      },
    }
  }
</script>

<style lang="scss">
.choice:hover{
  color: forestgreen;
  cursor: pointer;
}
</style>
