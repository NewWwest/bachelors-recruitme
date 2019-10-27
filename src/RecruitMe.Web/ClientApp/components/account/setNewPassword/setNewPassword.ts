import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../../services/user.service';
import { IRegistrationRequest, IResetPasswordRequest, ISetNewPassword } from '../../../models/user.models';

// @ts-ignore
@Component
export default class SetNewPassword extends Vue {
    newPassword: string = "";
    confirmPassword: string = "";

    submitted: boolean = false;
    fetching: boolean = false;
    newPasswordSet: boolean = false;
    backendError: string = "";

    userService: UserService = new UserService();

    constructor() {
        super();
    }

    handleSubmit() {
        this.submitted = true;
        this.fetching = true;

        let resetModel: ISetNewPassword = {
            token: this.$route.query.token,
            password: this.newPassword,
            confirmPassword: this.confirmPassword
        }

        this.userService.setNewPassword(resetModel).then(
            (data) => {
                this.fetching = false;
                this.newPasswordSet = true;
            }, (err) => {
                this.fetching = false;
                this.backendError = "Something went wrong";
            }
        )
    }
}
