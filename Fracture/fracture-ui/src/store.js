import Vue from 'vue'
import Vuex from 'vuex'
import fractalImageStore from './stores/FractalImageStore.js'

Vue.use(Vuex)

export default new Vuex.Store({
  modules: {
    fractalImage: fractalImageStore
  }
})
