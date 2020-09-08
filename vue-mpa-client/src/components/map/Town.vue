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
        console.log('diplayTownMapWindow')
        alert(this.loc.Title+ '\r\n\r\n'+ this.loc.Description + '\r\n\r\nYou can :\r\n' + this.loc.Verbs);
      }
    }
  }
</script>

<style lang="scss">

.town{
  background-image: url('/assets/map/locations/townCenter.png');
  margin: auto;
  width: 174px;
  height: 198px;
  background-position-x: -18px;
}

</style>
