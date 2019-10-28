import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../../services/user.service';

// @ts-ignore
@Component
export default class Login extends Vue {
    login: string = "";
    password: string = "";

    submitted: boolean = false;
    fetching: boolean = false;
    failed: boolean = false;

    userService: UserService = new UserService();

    constructor() {
        super();
    }

    handleSubmit() {
        this.userService.login(this.login, this.password).then((r) => {
            this.$router.push('/');
            //TODO
            //FIRE EVENT
        }, (err) => {
            this.failed = true;
        })
    }
}
