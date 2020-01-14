import axios, { AxiosResponse } from 'axios';
import { LocalStorageService } from '../localStorage/localStorageService';
import { IRegistrationRequest, IResetPasswordRequest,
     ISetNewPassword, IRemindLoginRequest } from '../../models/userFormModel';
import { IProfileData } from '@/models/personalDataModel';
import { Request, session, Task } from 'nativescript-background-http';

export class ApiGateway {
    private baseURL = "http://192.168.0.2:5000"; // base url

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
    // recruitment
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
            '/api/Recruitment/Profile', this.authHeader());
    }
    public setProfileData(personalDataModel: IProfileData) : Promise<AxiosResponse> {
        console.log(personalDataModel);
        
        return this.makeRequest(RequestType.POST,
            '/api/Recruitment/PersonalData', personalDataModel, this.authHeader());
    }
    public getProfilePicture(fileId: number) : Promise<AxiosResponse> {
        return this.makeRequest(RequestType.GET,
            '/api/asset/image/' + fileId, this.authHeader());
    }
    public setProfilePicture(filePath: string, fileName: string) : Task {
        let opt = {
            "Content-Type": "application/octet-stream",
            "File-Name": fileName
        }

        let s = session("picture");
        const options: Request = {
            url: this.baseURL + '/api/Recruitment/ProfilePicture',
            method: 'POST',
            headers: Object.assign(opt, this.authHeader().headers),
            description: 'Trwa przesyłanie zdjęcia',
            androidAutoDeleteAfterUpload: true,
            androidNotificationTitle: "Przesyłanie zdjęcia",
            //androidDisplayNotificationProgress: false,
            //androidAutoClearNotification: false,
        }
        
        const data = {
            name: 'picture',
            filename: filePath,
            mimeType: "image/png"
        }

        return s.multipartUpload([data], options);
    }

    // payments
    public getPaymentLink() {
        let opt = {
            'Content-Type': 'application/json; charset=utf-8'
        }
        const headers =  {
            headers: Object.assign(opt, this.authHeader().headers) 
        }
        console.log(headers);

        return this.makeRequest(RequestType.POST,
            '/api/payment/processPayment', {}, headers);
    }
    public isPaymentDone() {
        return this.makeRequest(RequestType.GET, 
            '/api/payment/isPaymentDone', this.authHeader());
    }

    // recruitment
    public examsAndStatus() {
        return this.makeRequest(RequestType.GET,
            '/api/Recruitment/examsandstatus', this.authHeader());
    }

    /// private helpers
    private authHeader() {
        return {
            headers: {
                Authorization: `Bearer ${LocalStorageService.getJwtToken()}`,
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