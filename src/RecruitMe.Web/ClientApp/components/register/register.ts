import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../services/user.service';

// @ts-ignore
@Component
export default class Register extends Vue {
    email:string="";
    password: string="";
    confirmPassword: string="";

    submitted:boolean=false;
    fetching: boolean=false;

    userService: UserService = new UserService();

    constructor(){
        super();
    }

    handleSubmit(){
        this.userService.register(this.email, this.password, this.confirmPassword);
    }
}
