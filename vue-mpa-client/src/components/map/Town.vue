<template>
    <div class="town" @click="diplayTownMapWindow()" >
    </div>
</template>

<script>
  export default {
    name: 'Town',
    props: {
      loc: Object,
    },
    data() {
      return {
        outputVar: "Je suis utilisable dans tout le fichier avec this.outputVar",               
      }
    },
    methods: {
      diplayTownMapWindow() {
        alert("In town #" + this.loc.id);
      }
    }
  }
</script>

<style lang="scss">

.town{
  background-image: url(assets/map/locations/townCenter.png);
  margin: auto;
}

</style>
