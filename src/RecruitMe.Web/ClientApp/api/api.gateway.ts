import axios from 'axios';
import { saveAs } from 'file-saver';
import { IPersonalData, IProfileData } from '../models/recruit.models';
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

    public getProfile() {
        return axios.get('/api/Recruitment/Profile', this.authHeader())
    }

    public setNewProfilePicture(fileName: string, file: any) {
        let data: FormData = new FormData();
        data.append('picture', file, fileName);

        return axios.post('/api/Recruitment/ProfilePicture', data, this.authHeader());
    }
    
    public uploadDocument(fileName: string, file: any) {
        let data: FormData = new FormData();
        data.append('file', file, fileName);

        return axios.post('/api/Recruitment/document', data, this.authHeader());
    }

    public deleteDocument(fileId: number) {
        return axios.delete(`/api/Recruitment/document/${fileId}`, this.authHeader());
    }

    public getImage(fileId: number) {
        return axios.get(`/api/asset/image/${fileId}`, this.authHeader()).then(resp => {
            return resp.data
        });
    }

    public downloadDocument(fileId: number, filename: string) {
        return axios.get(`/api/asset/${fileId}`, this.blobResponseAuthHeader()).then((response) => {
            saveAs(new Blob([response.data]), filename)
        });
    }

    // Payments calls

    public makePayment() {
        let data: any = {};

        return axios.post('/api/payment/processPayment', data, this.authHeader());
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
    private blobResponseAuthHeader(): any {
        return {
            responseType: "blob",
            headers: {
                Authorization: `Bearer ${LocalStorageService.getJwtToken()}`
            }
        };
    }
}