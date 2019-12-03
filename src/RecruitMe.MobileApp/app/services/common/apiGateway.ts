import axios, { AxiosResponse } from 'axios';
import { LocalStorageService } from '../localStorage/localStorageService';
import { IRegistrationRequest, IResetPasswordRequest,
     ISetNewPassword, IRemindLoginRequest } from '../../models/userFormModel';
import { IProfileData } from '@/models/personalDataModel';

export class ApiGateway {
    private baseURL = "http://192.168.0.2:5000"; // base url
    // private indicator = new LoadingIndicator();
    // private options: OptionsCommon = {
    //     message: 'Ladowanie...',
    //     details: 'aaa',
    //     mode: Mode.AnnularDeterminate,
    //     android: {
    //         dimBackground: true,
    //         cancelable: false, 
    //     } 
    // }

    private makeRequest(type: RequestType, url: string, data: any, headers?: any): Promise<AxiosResponse> {
        url = this.baseURL + url;
        //this.indicator.show(this.options);

        switch(type) {
            case RequestType.GET: 
               return axios.get(url, data).then( (response) => {
                   //this.indicator.hide();
                   return response;
               });
            case RequestType.POST:
               return axios.post(url, data, headers).then( (response) => {
                   //this.indicator.hide();
                   return response;
               });
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

    public register(registrationModel: IRegistrationRequest): Promise<AxiosResponse> {
        return this.makeRequest(RequestType.POST, '/api/Account/Register', registrationModel)
    }

    public resetPassword(resetPasswordRequest: IResetPasswordRequest): Promise<AxiosResponse> {
        return this.makeRequest(RequestType.POST, 
            '/api/Account/ResetPassword', resetPasswordRequest)
    }

    public setNewPassword(resetPasswordRequest: ISetNewPassword): Promise<AxiosResponse> {
        return this.makeRequest(RequestType.POST, 
            '/api/Account/SetNewPassword', resetPasswordRequest)
    }

    public remindLogin(remindModel: IRemindLoginRequest): Promise<AxiosResponse> {
        return this.makeRequest(RequestType.POST, 
            '/api/Account/RemindLogin', remindModel)
    }

    public getProfileData() : Promise<AxiosResponse> {
        return this.makeRequest(RequestType.GET,
            '/api/Recruitment/PersonalData', this.authHeader());
    }

    public setProfileData(personalDataModel: IProfileData) : Promise<AxiosResponse> {
        console.log(personalDataModel);
        
        return this.makeRequest(RequestType.POST,
            '/api/Recruitment/PersonalData', personalDataModel, this.authHeader());
    }

    public getImageRequest(fileId: number) : any {
        return {
            url: this.baseURL + '/api/asset/' + fileId,
            method: 'GET',
            headers: this.authHeader().headers
        }
    }

    // public getUser(): Promise<AxiosResponse> {
    //     return this.makeRequest(RequestType.GET,
    //         '/api/Account/GetUserBaseInfo?id=' + LocalStorageService.getUserId(), this.authHeader());
    // }

    /// private helpers

    private authHeader() {
        return {
            headers: {
                Authorization: `Bearer ${LocalStorageService.getJwtToken()}`,
                //'Content-Type': 'application/json; charset=utf-8'
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