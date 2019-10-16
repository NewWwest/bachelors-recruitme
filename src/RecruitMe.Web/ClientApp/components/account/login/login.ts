import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../../services/user.service';

// @ts-ignore
@Component
export default class Login extends Vue {
    email: string = "";
    password: string = "";

    submitted: boolean = false;
    fetching: boolean = false;

    userService: UserService = new UserService();

    constructor() {
        super();
    }

    handleSubmit() {
        this.userService.login(this.email, this.password)
    }
}
