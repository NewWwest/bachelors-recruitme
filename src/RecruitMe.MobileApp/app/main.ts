import Vue from 'nativescript-vue'
import store from './store'

import Router, { Goto } from './services/router'
import RadSideDrawer from 'nativescript-ui-sidedrawer/vue'

import App from './components/App.vue'
import Home from './components/Home.vue'
import DrawerContent from './components/common/DrawerContent.vue'

import { handleOpenURL, AppURL } from 'nativescript-urlhandler';
import { Frame } from 'tns-core-modules/ui/frame'
import CandidateDashboard from './components/CandidateDashboard.vue';
import PopupFactory from './services/popupFactory'

const VueDevtools = require('nativescript-vue-devtools')

if(TNS_ENV !== 'production') {
  Vue.use(VueDevtools)
}

// Prints Vue logs when --env.production is *NOT* set while building
Vue.config.silent = (TNS_ENV === 'production')

Vue.use(Router);
Vue.use(RadSideDrawer);

handleOpenURL((appURL: AppURL) => {
  console.log('Got the following appURL', appURL);
  if (appURL.path.includes("/payments/thankyou")) {
    if (appURL.path.includes("error")) {
      PopupFactory.GenericErrorPopup('Błąd z systemu Dotpay: "' + appURL.path.slice(appURL.path.lastIndexOf('/') + 1) + '"');
    }
    else {
      PopupFactory.GenericSuccessPopup("Sukces! Udana transakcja!");
    }
  }
});

new Vue({
  store: store,
  render: h => h(App, [
    h(DrawerContent, { slot: 'drawerContent' }),
    h(Home, { slot: 'mainContent' })
  ])
}).$start()
