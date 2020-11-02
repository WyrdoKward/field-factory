import Vue from 'vue'
import App from './App.vue'
import store from '../../api/stores/location.module' // ca sert à qqch ?

Vue.config.productionTip = false
export const bus = new Vue();

new Vue({
  render: h => h(App),
  store
}).$mount('#app')
