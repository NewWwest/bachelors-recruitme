import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import { UserService } from '../../services/user.service';

@Component({})
export default class Navmenu extends Vue {
    @Prop()
    userLoggedIn: boolean | undefined;
    displayName: string | undefined;
    userService: UserService = new UserService();

    constructor() {
        super();
    }
    updated() {
        this.displayName = this.userService.getDisplayName();
    }
  
    mounted() {
        this.displayName = this.userService.getDisplayName();
    }

    logout() {
        this.userService.logout();
        this.$emit("user-logged-in", false);
        this.displayName = this.userService.getDisplayName();
        this.$forceUpdate();
    }
}
