import axios from 'axios';
import { IPersonalData } from '../models/recruit.models';
import { LocalStorageService } from '../services/localStorage.service';

export class ApiGateway {

    public login(email: string, password: string): any {
        return axios.post('/Account/Login', { email, password })
    }
    public register(email: string, password: string, confirmPassword: string): any {
        return axios.post('/Account/Register', { email, password, confirmPassword })
    }

    public updatePersonalData(data: IPersonalData) {
        return axios.post('/Recruitment/UpdatePersonalData', data, this.authHeader())
    }

    private authHeader(){
        return {
            headers: {
                Authorization: `Bearer ${LocalStorageService.getJwtToken()}`
            }
        }
    }
}