import Vue from 'vue';
import { Component, Watch } from 'vue-property-decorator';
import { UserService } from '../../services/user.service';
import 'vuetify/dist/vuetify.min.css';
import 'material-design-icons-iconfont/dist/material-design-icons.css'
import 'material-design-icons-iconfont/dist/material-design-icons.css'
import { MessageBusService } from '../../services/messageBus.service';

@Component({
    components: {
        MenuComponent: require('../navmenu/navmenu.vue.html').default
    }
})
export default class AppComponent extends Vue {
    userLoggedIn: boolean = false;
    displayName: string = "";

    snackbar: boolean = false;
    errorMessage: string = "";

    mounted() {
        this.userLoggedIn = new UserService().isLoggedIn();
        this.displayName = new UserService().getDisplayName();
        MessageBusService.onError(this.showError);
        MessageBusService.onUserChanged(this.setCurretUser);
    }

    setCurretUser(state: boolean) {
        this.userLoggedIn = state;
        this.displayName = new UserService().getDisplayName();
        if (this.$route.path !== "" && this.$route.path !== "/") {
            this.$router.push("/");
        }
        this.$forceUpdate();
    }

    showError(evt: any) {
        this.snackbar = true;
        this.errorMessage = evt;
    }

    @Watch('$route', { immediate: true, deep: true })
    onUrlChange(to: any) {
        let prefix: string = "";
        const title: string = "RecruitMe";
        
        if (to.path.includes("adminPanel")) {
            prefix = "Admin Panel - ";
        }
        else {
            prefix = to.meta.title + " - ";
        }

        document.title = prefix + title;
    }
}
