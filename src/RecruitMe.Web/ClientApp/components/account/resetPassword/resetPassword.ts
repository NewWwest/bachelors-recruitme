import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../../services/user.service';
import { IResetPasswordRequest } from '../../../models/user.models';
import { ValidationService } from '../../../services/validation.service';
import { getErrorMessage } from '../../../helpers/error.helper';

@Component({})
export default class ResetPassword extends Vue {
    notEmptyRule: any = ValidationService.notEmptyRule();
    login: string = "";

    fetching: boolean = false;
    passwordResetCompleted: boolean = false;
    snackbar: boolean = false;
    errorMessage: string = "";

    userService: UserService = new UserService();

    constructor() {
        super();
    }

    handleSubmit() {
        this.fetching = true;

        let resetModel: IResetPasswordRequest = {
            login: this.login,
        }

        this.userService.resetPassword(resetModel).then(
            (data) => {
                this.fetching = false;
                this.passwordResetCompleted = true;
            }, (err) => {
                this.fetching = false;
                this.snackbar = true;
                this.errorMessage = getErrorMessage(err);
            }
        )
    }
}
