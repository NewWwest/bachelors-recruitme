import Vue from 'vue';
import { Component } from 'vue-property-decorator';


@Component
export default class Login extends Vue {
    email:string="";
    password: string="";
    submitted:boolean=false;
    fetching: boolean=false;
    // @Inject()
    // private _userService: UserService;
    constructor(){
        super()
    }

    handleSubmit(){
        console.log(this.email);
        console.log(this.password);
    }
}
