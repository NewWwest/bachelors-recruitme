import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import { UserService } from '../../services/user.service';
import { MessageBusService } from '../../services/messageBus.service';

@Component({})
export default class Navmenu extends Vue {
    userService: UserService = new UserService();

    @Prop()
    userLoggedIn: boolean | undefined;
    @Prop()
    displayName: string | undefined;

    constructor() {
        super();
    }

    logout() {
        this.userService.logout();
        this.$forceUpdate();
    }
}
