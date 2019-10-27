import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../../services/user.service';
import { IRegistrationRequest, IResetPasswordRequest } from '../../../models/user.models';

// @ts-ignore
@Component
export default class ResetPassword extends Vue {
    login: string = "";

    submitted: boolean = false;
    fetching: boolean = false;
    passwordResetCompleted: boolean = false;
    backendError: string = "";

    userService: UserService = new UserService();

    constructor() {
        super();
    }

    handleSubmit() {
        this.submitted = true;
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
                this.backendError = "Something went wrong";
            }
        )
    }
}
