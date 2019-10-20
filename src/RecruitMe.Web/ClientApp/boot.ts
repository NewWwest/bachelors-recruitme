import './css/site.css';
import 'bootstrap';
import Vue from 'vue';
import VueRouter from 'vue-router';
Vue.use(VueRouter);

const routes: any[] = [
    { path: '/', component: require('./components/home/home.vue.html') },
    { path: '/login', component: require('./components/account/login/login.vue.html') },
    { path: '/register', component: require('./components/account/register/register.vue.html') },

    { path: '/recruitment/profile', component: require('./components/recruitment/profile/profile.vue.html') },
];

new Vue({
    el: '#app-root',
    router: new VueRouter({ mode: 'history', routes: routes }),
    render: h => h(require('./components/app/app.vue.html'))
});