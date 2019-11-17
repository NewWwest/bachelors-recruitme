import './css/site.css';
import 'bootstrap';
import Vue from 'vue';
import VueRouter from 'vue-router';
import Vuetify from 'vuetify'

Vue.use(VueRouter);
Vue.use(Vuetify)

const routes: any[] = [
    { path: '/', component: require('./components/home/home.vue.html').default },
    { path: '/account/login', component: require('./components/account/login/login.vue.html').default },
    { path: '/account/register', component: require('./components/account/register/register.vue.html').default },
    { path: '/account/EmailVerified', component: require('./components/account/emailverified/emailverified.vue.html').default },

    { path: '/account/resetPassword', component: require('./components/account/resetPassword/resetPassword.vue.html').default },
    { path: '/account/setnewPassword', component: require('./components/account/setNewPassword/setNewPassword.vue.html').default },
    { path: '/account/remindLogin', component: require('./components/account/remindLogin/remindLogin.vue.html').default },
    
    { path: '/recruitment/profile', component: require('./components/recruitment/profile/profile.vue.html').default },
];

new Vue({
    el: '#app-root',
    router: new VueRouter({ mode: 'history', routes: routes }),
    render: h => h(require('./components/app/app.vue.html').default),
});