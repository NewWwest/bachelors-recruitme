import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import 'vuetify/dist/vuetify.min.css';
import 'material-design-icons-iconfont/dist/material-design-icons.css'

@Component({
    components: {
        MenuComponent: require('../navmenu/navmenu.vue.html').default
    }
})
export default class AppComponent extends Vue {
}
