<template>
    <div class="locationEvent" >
        <h2>{{event.text}}</h2>
        <ul v-if="event.choices" class="choices">
            <li v-for="choice in event.choices" :key="choice.id">
                <span @click="processChoice(choice.nextStepId)">{{choice.text}}</span>
            </li>
        </ul>
        <ul v-if="event.outcomes" class="outcomes">
            <li v-for="outcome in event.outcomes" :key="outcome.id">
                <span @click="processOutcome()">{{outcome.text}}</span>
            </li>
        </ul>
    </div>
</template>

<script>
import step1 from '@/utils/dummyLocationEventStep1.json'
  export default {
    name: 'LocationEvent',
    props: {
      event : Object
    },
    data() {
      return {
        outputVar: "Je suis utilisable dans tout le fichier avec this.outputVar",               
      }
    },
    methods: {
      processChoice(nextStepId) {
        console.log('Next Step is #'+nextStepId);
        // GET /event/{eventId}/step/{nextStepId}
        this.event = step1;

      },

      processOutcome() {
        return this.outputVar;
      }
    }
  }
</script>

<style lang="scss">
</style>
