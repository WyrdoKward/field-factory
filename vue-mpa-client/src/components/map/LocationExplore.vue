<template>
    <div class="locationEvent" >
        <h2>{{explore.Event.Title}}</h2>
        <div v-for="step in explore.Event.Steps" :key="step.Id" class="eventStep" >
          <h3>{{step.Text}}</h3>
          <ul v-if="step.Choices" class="choices">
              <li v-for="choice in explore.Choices" :key="choice.Id">
                  <span @click="processChoice(choice.Id)">{{choice.Text}}</span>
              </li>
          </ul>
          <div v-else-if="step.Outcome" class="outcome">
              <p >{{step.Outcome.text}}</p>
              <p v-if="explore.Outcome.NextStepId" @click="processOutcome(explore.Outcome.NextStepId)">Continuer...</p>
              <p v-else>QUETE TERMINEE</p>
              <!--<li v-for="outcome in explore.outcomes" :key="outcome.id">
                  <span @click="processOutcome()">{{outcome.text}}</span>
              </li>-->
          </div>
          <p v-else>Problème: ni outcome ni choices</p>
        </div>
         <button type="button" @click="resetQuest()" style="margin-bottom: 80px;">DEBUG : RESET QUEST</button> 
    </div>
</template>

<script>

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
      //locationID : string
    },
    data() {
      return {
      }
    },
    methods: {
      processChoice(choiceId) {
        console.log('Location: #'+explore.IdLocation);
        console.log('Choice: #'+choiceId);
        // PUT: api/Explore/
        // {"IdLocation": "dummyLocation", "IdChoice": 2}
        const json = JSON.stringify({IdLocation: explore.IdLocation,  IdChoice: choiceId});
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
        // Vérification du timer coté server
        this.event = steps[nextStepId];
        var rnd = this.getRandom(Object.keys(this.explore.outcomes).length);
        console.log('Outcome generated : '+rnd);
        this.explore.outcome = this.explore.outcomes[rnd];

        console.log(this.event);

      },

      processOutcome(nextStepId) {
        //On get juste le step suivant
        // Soit c'est fini
        // Soit y'a un choix immédiat à faire
        this.event = steps[nextStepId];
        
      },
      getRandom(max){
          return Math.floor(Math.random() * (max));
      },
      resetQuest(){
          this.event = "{}";
      }

    }
  }
</script>

<style lang="scss">
</style>
