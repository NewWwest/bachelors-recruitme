import axios from 'axios';

export class ApiGateway {
    public accountLogin(email:string, password:string):any {
        return axios.post('/Account/Login',{ email, password })
    }
    public accountRegister(email:string, password:string, confirmPassword: string):any{
        return axios.post('/Account/Register',{ email, password, confirmPassword })
    }
}