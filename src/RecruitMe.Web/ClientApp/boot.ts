import './css/site.css';
import 'bootstrap';
import Vue from 'vue';
import VueRouter from 'vue-router';
import Vuetify from 'vuetify';
//@ts-ignore - package does not provide types definition but we reference it only here
import DatetimePicker from 'vuetify-datetime-picker/dist'

Vue.use(DatetimePicker)
Vue.use(Vuetify)
Vue.use(VueRouter);
Vue.component('card-layout', require('./components/shared/CardLayout/cardLayout.vue.html').default)

const vuetify =  new Vuetify({
    theme: {
        themes: {
            light: {
                primary: '#673ab7',
                secondary: '#3f51b5',
                accent: '#2196f3',
                error: '#03a9f4',
                warning: '#00bcd4',
                info: '#e91e63',
                success: '#4caf50'
            },
        },
    },
    icons: {
        iconfont: 'md',
    }
});

const routes: any[] = [
    { path: '/', component: require('./components/home/home.vue.html').default },
    { path: '/account/login', component: require('./components/account/login/login.vue.html').default },
    { path: '/account/register', component: require('./components/account/register/register.vue.html').default },
    { path: '/account/EmailVerified', component: require('./components/account/emailverified/emailverified.vue.html').default },

    { path: '/account/resetPassword', component: require('./components/account/resetPassword/resetPassword.vue.html').default },
    { path: '/account/setnewPassword', component: require('./components/account/setNewPassword/setNewPassword.vue.html').default },
    { path: '/account/remindLogin', component: require('./components/account/remindLogin/remindLogin.vue.html').default },
    
    { path: '/recruitment/profile', component: require('./components/recruitment/profile/profile.vue.html').default },

    { path: '/payments/makepayments', component: require('./components/payments/makePayment/makePayment.vue.html').default },
    { path: '/payments/thankyou', component: require('./components/payments/thankYou/thankYou.vue.html').default },
    
    { path: '/adminPanel/manage/:entityType', component: require('./components/adminpanel/manage/manage.vue.html').default },
    { path: '/adminPanel/add/:entityType', component: require('./components/adminpanel/add/add.vue.html').default },
    { path: '/adminPanel/details/:entityType/:id', component: require('./components/adminpanel/details/details.vue.html').default },
];

new Vue({
    vuetify: vuetify,
    el: '#app-root',
    router: new VueRouter({ mode: 'history', routes: routes }),
    render: h => h(require('./components/app/app.vue.html').default),
    components: {

    }
});