<template>
    <div class="locationEvent" >
        <h2>{{event.text}}</h2>
        <ul v-if="event.choices" class="choices">
            <li v-for="choice in event.choices" :key="choice.id">
                <span @click="processChoice(choice.nextStepId)">{{choice.text}} (nextStep: {{choice.nextStepId}}</span>
            </li>
        </ul>
        <div v-else-if="event.outcome" class="outcome">
            <p >{{event.outcome.text}}</p>
            <p v-if="event.outcome.nextStepId" @click="processOutcome(event.outcome.nextStepId)">Continuer...</p>
            <p v-else>QUETE TERMINEE</p>
            <!--<li v-for="outcome in event.outcomes" :key="outcome.id">
                <span @click="processOutcome()">{{outcome.text}}</span>
            </li>-->
        </div>
        <p v-else>Problème: ni outcome ni choices</p>
         <button type="button" @click="resetQuest()" style="margin-bottom: 80px;">DEBUG : RESET QUEST</button> 
    </div>
</template>

<script>
/*
import { activeCountries } from '@/utils/CountryConstants'

const homepageComponents = {}

activeCountries.map(country => {
  const pageName = `HomePage${country.code.toUpperCase()}`
  homepageComponents[pageName] = () => import(`@/pages/homepage/${pageName}`)
})

*/

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
      event : Object
    },
    data() {
      return {
      }
    },
    methods: {
      processChoice(nextStepId) {
        console.log('Next Step is #'+nextStepId);
        // GET /event/{eventId}/step/{nextStepId}
        //on selectionne un seul outcome random coté serveur
        this.event = steps[nextStepId];
        var rnd = this.getRandom(Object.keys(this.event.outcomes).length);
        console.log('Outcome generated : '+rnd);
        this.event.outcome = this.event.outcomes[rnd];

        console.log(this.event);

      },

      processOutcome(nextStepId) {
        //On get juste le step suivant
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
