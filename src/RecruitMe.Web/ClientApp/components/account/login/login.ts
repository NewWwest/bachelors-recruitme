import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../../services/user.service';
import { ValidationService } from '../../../services/validation.service';

@Component({})
export default class Login extends Vue {
    notEmptyRule: any = ValidationService.notEmptyRule();
    login: string = "";
    password: string = "";

    fetching: boolean = false;

    userService: UserService = new UserService();

    handleSubmit() {
        if (this.fetching)
            return;

        this.fetching = true;
        this.userService.login(this.login, this.password).then((r) => {
            this.fetching = false;
        }, (err) => {
            this.fetching = false;
        })
    }
}
