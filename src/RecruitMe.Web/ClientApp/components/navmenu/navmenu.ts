import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import { UserService } from '../../services/user.service';

@Component({})
export default class Navmenu extends Vue {
    @Prop()
    userLoggedIn: boolean | undefined;
    @Prop()
    displayName: string | undefined;
    userService: UserService = new UserService();

    constructor() {
        super();
    }

    logout() {
        this.userService.logout();
        this.$emit("user-logged-in", false);
        this.$forceUpdate();
    }
}
