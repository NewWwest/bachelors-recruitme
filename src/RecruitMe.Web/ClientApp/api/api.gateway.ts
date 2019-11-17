import axios from 'axios';
import { IPersonalData } from '../models/recruit.models';
import { LocalStorageService } from '../services/localStorage.service';
import { IRegistrationRequest, IResetPasswordRequest, ISetNewPassword, IRemindLoginRequest } from '../models/user.models';

export class ApiGateway {

    public login(candidateId: string, password: string): any {
        let clientsecret: string = "123456789ABCDEF123456789ABCDEF123456789ABCDEF123456789ABCDEF";
        let data: string = "grant_type=password&" +
            "client_id=client&" +
            `client_secret=${clientsecret}&` +
            "scope=api-recruit-me&" +
            `username=${encodeURIComponent(candidateId)}&` +
            `password=${encodeURIComponent(password)}`
        return axios.post('/connect/token', data, this.ContentTypeFormUrlencoded())
    }

    public register(registrationModel: IRegistrationRequest): any {
        return axios.post('/api/Account/Register', registrationModel)
    }

    public resetPassword(resetPasswordRequest: IResetPasswordRequest): any {
        return axios.post('/api/Account/ResetPassword', resetPasswordRequest)
    }

    public setNewPassword(resetPasswordRequest: ISetNewPassword): any {
        return axios.post('/api/Account/SetNewPassword', resetPasswordRequest)
    }

    public remindLogin(remindModel: IRemindLoginRequest): any {
        return axios.post('/api/Account/RemindLogin', remindModel)
    }

    public updatePersonalData(data: IPersonalData) {
        return axios.post('/api/Recruitment/PersonalData', data, this.authHeader())
    }

    public getPersonalData() {
        return axios.get('/api/Recruitment/PersonalData', this.authHeader())
    }

    public setNewProfilePicture(fileName:string, file:any) {
        let data: string = `picture=${file}`;

        return axios.post('/api/Recruitment/ProfilePicture', data, this.authHeader());
    }

    private authHeader() {
        return {
            headers: {
                Authorization: `Bearer ${LocalStorageService.getJwtToken()}`
            }
        }
    }
    private ContentTypeFormUrlencoded() {
        return {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        };
    }
}