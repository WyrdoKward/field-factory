<template>
  <div>
    <!--deadLine: {{deadline}} - {{remainingTime}}<br />
    lastDeadline = {{this.lastDeadline}}<br />-->
    <div class="text-center" v-if="remainingTime">
        <span v-if="days">{{ days }}:</span ><span v-if="hours">{{ hours | formatTime }}:</span><span>{{ minutes | formatTime }}:{{ seconds | formatTime }}</span><br />
     </div>
    <div class="text-center" v-if="!remainingTime">
      Terminé
    </div>
  </div>
</template>

<script>
export default {
  /*https://dev.to/rachel_cheuk/timer-component-for-vue-js-3aad*/
  name : "Timer",
  props: {
    deadline: {
      type: String,
      required: true,
    },
    speed: {
      type: Number,
      default: 1000,
    },
  },
  data() {
    return {
      lastDeadline : this.deadline,
      remainingTime: Date.parse(this.deadline) - Date.parse(new Date()) //in ms
    };
  },
  watch: {
    deadline: function(newValue, oldValue) {
      console.log("WATCH !");
      console.log(newValue, oldValue);
      this.lastDeadline = newValue;
      this.remainingTime = Date.parse(this.deadline) - Date.parse(new Date());
      console.log('remaining time = '+this.remainingTime);
      setTimeout(this.countdown, this.speed);
    }
  },
  mounted() {
    setTimeout(this.countdown, 1000);
  },
  computed: {
    seconds() {
      return Math.floor((this.remainingTime / 1000) % 60);
    },
    minutes() {
      return Math.floor((this.remainingTime / 1000 / 60) % 60);
    },
    hours() {
      return Math.floor((this.remainingTime / (1000 * 60 * 60)) % 24);
    },
    days() {
      return Math.floor(this.remainingTime / (1000 * 60 * 60 * 24));
    }
  },
  filters: {
    formatTime(value) {
      if (value < 10) {
        return "0" + value;
      }
      return value;
    }
  },
  methods: {
    countdown() {
      this.remainingTime = Date.parse(this.deadline) - Date.parse(new Date());
      
      if (this.remainingTime > 0) {
        setTimeout(this.countdown, this.speed);
      } else {
        console.log('Finish');
        this.remainingTime = null;
        //setTimeout(this.hasDeadlineChanged, 1000);
        this.$emit('onFinish')
      }
    },
    /*hasDeadlineChanged(){ //Remplacer ca par un event au lieu d'apeller ca ttes les secondes?
      console.log('HasChanged ?') 
      setTimeout(this.hasDeadlineChanged, 1000);
      if(this.lastDeadline != this.deadline){
        console.log('DeadLine Changed !')
        this.lastDeadline = this.deadline
        this.remainingTime = Date.parse(this.deadline) - Date.parse(new Date());
        console.log('reemaining time = '+this.remainingTime)
        setTimeout(this.countdown, this.speed);
      }
    }*/
  }
}
</script>