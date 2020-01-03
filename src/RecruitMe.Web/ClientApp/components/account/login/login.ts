import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../../services/user.service';
import { ValidationService } from '../../../services/validation.service';
import { getErrorMessage } from '../../../helpers/error.helper';

@Component({})
export default class Login extends Vue {
    notEmptyRule: any = ValidationService.notEmptyRule();
    login: string = "";
    password: string = "";

    fetching: boolean = false;
    snackbar: boolean = false;
    errorMessage: string = "";

    userService: UserService = new UserService();

    constructor() {
        super();
    }

    handleSubmit() {
        if (this.fetching)
            return;

        this.fetching = true;
        this.userService.login(this.login, this.password).then((r) => {
            this.$emit("user-logged-in", true);
            this.fetching = false;
        }, (err) => {
            this.fetching = false;
            this.snackbar = true;
            this.errorMessage = getErrorMessage(err, "Logowanie się nie powiodło, spróbuj jeszcze raz lub skontaktuj się z administratorem");
        })
    }
}
