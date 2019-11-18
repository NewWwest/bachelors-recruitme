import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../../services/user.service';
import { IRegistrationRequest } from '../../../models/user.models';

@Component({})
export default class Register extends Vue {
    email: string = "";
    password: string = "";
    confirmPassword: string = "";
    name: string = "";
    surname: string = "";
    pesel: string = "";
    noPesel: boolean = false;
    birthDate: Date | null = null;

    submitted: boolean = false;
    fetching: boolean = false;
    registrationCompleted: boolean = false;
    backendError: string = "";

    userService: UserService = new UserService();

    constructor() {
        super();
    }

    handleSubmit() {
        this.submitted = true;
        this.fetching = true;

        let registrationModel: IRegistrationRequest = {
            email: this.email,
            password: this.password,
            confirmPassword: this.confirmPassword,
            name: this.name,
            surname: this.surname,
            pesel: this.noPesel ? null : this.pesel,
            noPesel: this.noPesel,
            birthDate: this.birthDate as Date
        }

        this.userService.register(registrationModel).then(
            (data) => {
                this.fetching = false;
                this.registrationCompleted = true;
            }, (err) => {
                this.fetching = false;
                this.backendError = "Something went wrong";
            }
        )
    }
}
