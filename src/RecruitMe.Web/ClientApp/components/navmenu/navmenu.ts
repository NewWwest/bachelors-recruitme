import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import { UserService } from '../../services/user.service';

@Component({})
export default class Navmenu extends Vue {
    userService: UserService = new UserService();

    @Prop()
    userLoggedIn: boolean | undefined;
    @Prop()
    displayName: string | undefined;
    @Prop()
    messages: number | undefined;

    constructor() {
        super();
    }

    logout() {
        this.userService.logout();
        this.$emit("user-logged-in", false);
        this.$forceUpdate();
    }
}
