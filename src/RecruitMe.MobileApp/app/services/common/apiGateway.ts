import axios, { AxiosResponse } from 'axios';
import { LocalStorageService } from '../userService/localStorageService';
import { IRegistrationRequest, IResetPasswordRequest,
     ISetNewPassword, IRemindLoginRequest } from '../../models/userFormModel';

export class ApiGateway {
    private baseURL = "192.168.0.2"; // base url

    private makeRequest(type: RequestType, url: string, data: any, headers?: any): Promise<AxiosResponse> {
        url = this.baseURL + url;
        
        switch(type) {
            case RequestType.GET: 
               return axios.get(url, data);
            case RequestType.POST:
               return axios.post(url, data, headers);
            default: throw new Error("something bad has happened");
        }
    }

    // API METHODS

    public login(candidateId: string, password: string): Promise<AxiosResponse> {
        let clientsecret: string = "123456789ABCDEF123456789ABCDEF123456789ABCDEF123456789ABCDEF";
        let data: string = "grant_type=password&" +
            "client_id=client&" +
            `client_secret=${clientsecret}&` +
            "scope=api-recruit-me&" +
            `username=${encodeURIComponent(candidateId)}&` +
            `password=${encodeURIComponent(password)}`
        return this.makeRequest(RequestType.POST, '/connect/token', data, this.ContentTypeFormUrlencoded());
    }

    public register(registrationModel: IRegistrationRequest): any {
        return this.makeRequest(RequestType.POST, '/api/Account/Register', registrationModel)
    }

    public resetPassword(resetPasswordRequest: IResetPasswordRequest): any {
        return this.makeRequest(RequestType.POST, 
            '/api/Account/ResetPassword', resetPasswordRequest)
    }

    public setNewPassword(resetPasswordRequest: ISetNewPassword): any {
        return this.makeRequest(RequestType.POST, 
            '/api/Account/SetNewPassword', resetPasswordRequest)
    }

    public remindLogin(remindModel: IRemindLoginRequest): any {
        return this.makeRequest(RequestType.POST, 
            '/api/Account/RemindLogin', remindModel)
    }

    /// private helpers

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

enum RequestType {
    GET,
    POST
}