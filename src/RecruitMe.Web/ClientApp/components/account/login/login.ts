import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../../services/user.service';
import { ValidationService } from '../../../services/validation.service';

@Component({})
export default class Login extends Vue {
    notEmptyRule: any = ValidationService.notEmptyRule();
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
            this.$emit("user-logged-in", true);
        }, (err) => {
            this.failed = true;
        })
    }
}
