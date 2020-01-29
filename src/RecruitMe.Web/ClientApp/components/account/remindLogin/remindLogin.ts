import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../../services/user.service';
import { IRemindLoginRequest } from '../../../models/user.models';
import { ValidationService } from '../../../services/validation.service';

@Component({})
export default class RemindLogin extends Vue {
    notEmptyRule: any = ValidationService.notEmptyRule();
    peselHas11digit: any = ValidationService.peselRules();

    email: string = "";
    name: string = "";
    surname: string = "";
    pesel: string = "";
    noPesel: boolean = false;

    fetching: boolean = false;
    loginReminded: boolean = false;

    userService: UserService = new UserService();

    handleSubmit() {
        this.fetching = true;

        let remingLoginModel: IRemindLoginRequest;
        if (this.noPesel) {
            remingLoginModel = {
                email: this.email,
                name: this.name,
                surname: this.surname,
                pesel: null
            }
        }
        else {
            remingLoginModel = {
                email: this.email,
                name: null,
                surname: null,
                pesel: this.pesel
            }
        }

        this.userService.remindLogin(remingLoginModel).then(
            (data) => {
                this.fetching = false;
                this.loginReminded = true;
            }, (err) => {
                this.fetching = false;
            }
        )
    }
}
