import Vue from 'vue'
import Vuex from 'vuex'
import TestModule from '../store/modules/test-module'
import LocationModule from '../store/modules/location.module'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
  },
  mutations: {
  },
  actions: {
  },
  modules: {
    TestModule,
    LocationModule
  }
})