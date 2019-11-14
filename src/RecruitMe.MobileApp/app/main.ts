import Vue from 'nativescript-vue'
import store from './store'

import Router from './services/router'

import App from './components/App.vue'
import Home from './components/Home.vue'
import DrawerContent from './components/common/DrawerContent.vue'

const VueDevtools = require('nativescript-vue-devtools')

if(TNS_ENV !== 'production') {
  Vue.use(VueDevtools)
}

// Prints Vue logs when --env.production is *NOT* set while building
Vue.config.silent = (TNS_ENV === 'production')

Vue.use(Router);

new Vue({
  store: store,
  render: h => h(App, [
    h(DrawerContent, { slot: 'drawerContent' }),
    h(Home, { slot: 'mainContent' })
  ])
}).$start()
