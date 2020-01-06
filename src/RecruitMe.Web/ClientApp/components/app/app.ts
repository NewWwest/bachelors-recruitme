import Vue from 'vue';
import { Component, Watch } from 'vue-property-decorator';
import { UserService } from '../../services/user.service';
import 'vuetify/dist/vuetify.min.css';
import 'material-design-icons-iconfont/dist/material-design-icons.css'
import 'material-design-icons-iconfont/dist/material-design-icons.css'
import { MessageService } from '../../services/message.service';

@Component({
    components: {
        MenuComponent: require('../navmenu/navmenu.vue.html').default
    }
})
export default class AppComponent extends Vue {
    userLoggedIn: boolean = false;
    displayName: string = "";
    messages: number = 0;

    beforeMount() {
        setInterval(this.checkMessages, 20000);
    }

    mounted() {
        this.userLoggedIn = new UserService().isLoggedIn();
        this.displayName = new UserService().getDisplayName();
    }

    setCurretUser(state: boolean) {
        this.userLoggedIn = state;
        this.displayName = new UserService().getDisplayName();
        this.$forceUpdate();
        this.$router.push("/");
    }

    checkMessages() {
        if (!this.$route.path.includes('chat')) {
            new MessageService().checkNewMessages().then(d => {
                this.messages = d;
            });
        }
        else {
            this.messages = 0;
        }
    }
}
