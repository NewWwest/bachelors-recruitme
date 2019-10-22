import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../services/user.service';

// @ts-ignore
@Component
export default class Navmenu extends Vue {
    displayName: string = "";
    userService: UserService = new UserService();

    constructor() {
        super();
    }

    mounted() {
        //TODO FIX this to work after login
        this.displayName = this.userService.getDisplayName();
    }

    logout() {
        this.userService.logout();
        this.$forceUpdate();
    }
}
