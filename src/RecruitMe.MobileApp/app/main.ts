import Vue from 'nativescript-vue'
import Home from './components/Home.vue'

import VueDevtools from 'nativescript-vue-devtools'
import store from './store'

import Router from './services/router'

if(TNS_ENV !== 'production') {
  Vue.use(VueDevtools)
}

// Prints Vue logs when --env.production is *NOT* set while building
Vue.config.silent = (TNS_ENV === 'production')

Vue.use(Router);

new Vue({
  store: store,
  render: h => h('frame', [h(Home)])
}).$start()
