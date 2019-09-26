import { ApiGateway } from "../api/api.gateway";

export class UserService {
    constructor(private _apiGateway: ApiGateway){

    }

    public login(email:string, password:string) {
        this._apiGateway.accountLogin(email,password).then(
            (response:any )=>console.log(response),
            (err:any)=>console.error(err))
    }
}