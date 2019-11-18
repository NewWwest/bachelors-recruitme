import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../../services/user.service';
import { IRegistrationRequest } from '../../../models/user.models';

@Component({})
export default class Register extends Vue {
    notEmptyRule:any = [(v:string) => v!="" || "Musisz podać to pole"]
    peselHas11digit:any = [
        (v:string) => v.length==11 || "Pesel musi mieć 11 cyfr",
        (v:string) => /^\d+$/.test(v) || "Pesel może zawierać tylko cyfry",
    ]
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
                console.log(err);
                let lines = err.data.split('\n');

                this.backendError = lines.map((line:string)=>`<p>${line}</p>`).join('');
            }
        )
    }
}
