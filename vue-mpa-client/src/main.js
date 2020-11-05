import Vue from 'vue'
import App from './App.vue'
import store from './store' // ca sert à qqch ?
//http://localhost:8080/test
Vue.config.productionTip = false
export const bus = new Vue();

new Vue({
  render: h => h(App),
  store
}).$mount('#app')
