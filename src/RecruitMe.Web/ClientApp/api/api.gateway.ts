import axios from 'axios';
import { saveAs } from 'file-saver';
import { IPersonalData, IProfileData } from '../models/recruit.models';
import { LocalStorageService } from '../services/localStorage.service';
import { IRegistrationRequest, IResetPasswordRequest, ISetNewPassword, IRemindLoginRequest } from '../models/user.models';
import { IExamCategory, ITeacher } from '../models/administraion.models';

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

    public listExamCategories() {
        return axios.get(`/api/administration/examCategory`, this.authHeader()).then((resp) => {
            return resp.data;
        });
    }

    public getExamCategory(id: number) {
        return axios.get(`/api/administration/examCategory/${id}`, this.authHeader()).then((resp) => {
            return resp.data;
        });
    }

    public addExamCategory(data: IExamCategory) {
        return axios.put(`/api/administration/examCategory`, data, this.authHeader()).then((resp) => {
            return resp.data;
        });
    }

    public updateExamCategory(data: IExamCategory) {
        return axios.post(`/api/administration/examCategory`, data, this.authHeader()).then((resp) => {
            return resp.data;
        });
    }

    public deleteExamCategory(examCategoryId: number) {
        return axios.delete(`/api/administration/examCategory/${examCategoryId}`, this.authHeader()).then((resp) => {
            return resp.data;
        });
    }

    public listTeacherss() {
        return axios.get(`/api/administration/teacher`, this.authHeader()).then((resp) => {
            return resp.data;
        });
    }

    public getTeacher(id: number) {
        return axios.get(`/api/administration/teacher/${id}`, this.authHeader()).then((resp) => {
            return resp.data;
        });
    }

    public addTeacher(data: ITeacher) {
        return axios.put(`/api/administration/teacher`, data, this.authHeader()).then((resp) => {
            return resp.data;
        });
    }

    public updateTeacher(data: ITeacher) {
        return axios.post(`/api/administration/teacher`, data, this.authHeader()).then((resp) => {
            return resp.data;
        });
    }

    public deleteTeacher(teacherId: number) {
        return axios.delete(`/api/administration/teacher/${teacherId}`, this.authHeader()).then((resp) => {
            return resp.data;
        });
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