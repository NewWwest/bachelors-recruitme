import Vue from 'nativescript-vue'
import store from './store'

import Home from './components/Home.vue'
import Router from './services/router'

const VueDevtools = require('nativescript-vue-devtools')

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
