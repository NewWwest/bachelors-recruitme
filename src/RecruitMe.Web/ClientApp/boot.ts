import './css/site.css';
import 'bootstrap';
import Vue from 'vue';
import VueRouter from 'vue-router';
import Vuetify from 'vuetify';
//@ts-ignore - package does not provide types definition but we reference it only here
import DatetimePicker from 'vuetify-datetime-picker/dist'
import { AuthenticatedGuard } from './helpers/authenticated.guard';
import { AdminGuard } from './helpers/admin.guard';

Vue.use(DatetimePicker)
Vue.use(Vuetify)
Vue.use(VueRouter);
Vue.component('card-layout', require('./components/shared/cardlayout/cardLayout.vue.html').default)
Vue.component('spinner', require('./components/shared/spinner/spinner.vue.html').default)

const vuetify =  new Vuetify({
    theme: {
        themes: {
            light: {
                primary: '#2196f3',
                secondary: '#03a9f4',
                accent: '#3f51b5',
                error: '#e91e63',
                warning: '#ffc107',
                info: '#607d8b',
                success: '#8bc34a'
            }
        }
    },
    icons: {
        iconfont: 'md',
    }
});

const routes: any[] = [
    // home
    {
        path: '/', component: require('./components/staticpages/home.vue.html').default,
        meta: { title: "Strona główna" },
    },
    {
        path: '/terms', component: require('./components/staticpages/termsOfService.vue.html').default,
        meta: { title: "Regulamin" },
    },
    {
        path: '/about', component: require('./components/staticpages/about.vue.html').default,
        meta: { title: "O nas" },
    },

    // account
    {
        path: '/account/login', component: require('./components/account/login/login.vue.html').default,
        meta: { title: "Logowanie" },
    },
    { 
        path: '/account/register', component: require('./components/account/register/register.vue.html').default,
        meta: { title: "Rejestracja" }, 
    },
    { 
        path: '/account/EmailVerified', component: require('./components/account/emailverified/emailverified.vue.html').default, 
        meta: { title: "E-mail zweryfikowany" }, 
    },    
    { 
        path: '/account/resetPassword', component: require('./components/account/resetPassword/resetPassword.vue.html').default, 
        meta: { title: "Reset hasła" }, 
    },
    { 
        path: '/account/setnewPassword', component: require('./components/account/setNewPassword/setNewPassword.vue.html').default, 
        meta: { title: "Nowe hasło" }, 
    },
    { 
        path: '/account/remindLogin', component: require('./components/account/remindLogin/remindLogin.vue.html').default, 
        meta: { title: "Przypomnij login" }, 
    },
    
    // recruitment
    { 
        path: '/recruitment/profile', component: require('./components/recruitment/profile/profile.vue.html').default, 
        meta: { title: "Profil kandydata" }, beforeEnter: AuthenticatedGuard, 
    },

    // payments
    {
        path: '/payments/makepayment', component: require('./components/payments/makePayment/makePayment.vue.html').default,
        meta: { title: "Status płatności" }, beforeEnter: AuthenticatedGuard, 
    },
    { 
        path: '/payments/thankyou', component: require('./components/payments/thankYou/thankYou.vue.html').default, 
        meta: { title: "Dziękujemy za płatność" }, beforeEnter: AuthenticatedGuard, 
    },
    
    // admin panel
    { 
        path: '/adminPanel/manage/:entityType', component: require('./components/adminpanel/manage/manage.vue.html').default, 
        meta: { title: "Zarządzaj" }, beforeEnter: AdminGuard, 
    },
    { 
        path: '/adminPanel/add/:entityType', component: require('./components/adminpanel/add/add.vue.html').default, 
        meta: { title: "Dodaj" }, beforeEnter: AdminGuard, 
    },
    { 
        path: '/adminPanel/details/:entityType/:id', component: require('./components/adminpanel/details/details.vue.html').default, 
        meta: { title: "Szczegóły" }, beforeEnter: AdminGuard, 
    },
    {
        path: '/chatwith/:login', component: require('./components/chat/chatWith.vue.html').default,
        meta: { title: "Chat" }, beforeEnter: AuthenticatedGuard, 
    }
];

new Vue({
    vuetify: vuetify,
    el: '#app-root',
    router: new VueRouter({ mode: 'history', routes: routes }),
    render: h => h(require('./components/app/app.vue.html').default)
});