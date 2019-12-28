import axios from 'axios';
import { saveAs } from 'file-saver';
import { IPersonalData, IProfileData } from '../models/recruit.models';
import { LocalStorageService } from '../services/localStorage.service';
import { IRegistrationRequest, IResetPasswordRequest, ISetNewPassword, IRemindLoginRequest } from '../models/user.models';
import { IExamCategory, ITeacher, IExam, IExamTaker } from '../models/administraion.models';

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

    public getProfile(id: number | undefined = undefined) {
        if (id) {
            return axios.get(`/api/administration/candidates/${id}`, this.authHeader()).then((resp) => {
                return resp.data;
            });
        }
        else {
            return axios.get('/api/Recruitment/Profile', this.authHeader())
        }
    }

    public setNewProfilePicture(fileName: string, file: any) {
        let data: FormData = new FormData();
        data.append('picture', file, fileName);

        return axios.post('/api/Recruitment/ProfilePicture', data, this.authHeader());
    }

    public examsAndStatus() {
        return axios.get('/api/Recruitment/examsandstatus', this.authHeader());
    }
    
    public uploadDocument(fileName: string, file: any) {
        let data: FormData = new FormData();
        data.append('file', file, fileName);

        return axios.post('/api/Recruitment/document', data, this.authHeader());
    }

    public deleteDocument(fileId: number, userId: number | undefined = undefined) {
        if (userId) {
            return axios.delete(`/api/administration/candidates/${userId}/documents/${fileId}`, this.authHeader());
        }
        else {
            return axios.delete(`/api/Recruitment/document/${fileId}`, this.authHeader());
        }
    }

    public getImage(fileId: number) {
        return axios.get(`/api/asset/image/${fileId}`, this.authHeader()).then(resp => {
            return resp.data
        });
    }

    public downloadDocument(fileId: number, filename: string, userId: number | undefined = undefined) {
        let url = userId ? `/api/asset/${fileId}?userId=${userId}` : `/api/asset/${fileId}`;

        return axios.get(url, this.blobResponseAuthHeader()).then((response) => {
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

    public listTeachers() {
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


    public listExams() {
        return axios.get(`/api/administration/exam`, this.authHeader()).then((resp) => {
            return resp.data;
        });
    }

    public listExamsForUser(userId: number | undefined = undefined) {
        if (userId) {
            return axios.get(`/api/administration/candidates/${userId}/exams`, this.authHeader()).then((resp) => {
                return resp.data;
            });
        }
        else {
            alert("TODO")
            return null as any as Promise<any>;
        }
    }

    public getExam(id: number) {
        return axios.get(`/api/administration/exam/${id}`, this.authHeader()).then((resp) => {
            return resp.data;
        });
    }

    public addExam(data: IExam) {
        return axios.put(`/api/administration/exam`, data, this.authHeader()).then((resp) => {
            return resp.data;
        });
    }

    public addOrUpdateExamTaker(data: IExamTaker) {
        return axios.post(`/api/administration/candidates/${data.userId}/exams`, data, this.authHeader()).then((resp) => {
            return resp.data;
        });
    }
    public deleteExamTaker(userId:number, id: number) {
        return axios.delete(`/api/administration/candidates/${userId}/exams/${id}`, this.authHeader()).then((resp) => {
            return resp.data;
        });
    }
    public updateExam(data: IExam) {
        return axios.post(`/api/administration/exam`, data, this.authHeader()).then((resp) => {
            return resp.data;
        });
    }

    public deleteExam(ExamId: number) {
        return axios.delete(`/api/administration/exam/${ExamId}`, this.authHeader()).then((resp) => {
            return resp.data;
        });
    }

    public listCandidates(pagingParams: any) {
        return axios.get(`/api/administration/candidates/`, this.withParams(pagingParams)).then((resp) => {
            return resp.data;
        });
    }

    public updateProfile(data: IProfileData) {
        let json = {
            id: data.id,
            userId: data.userId,

            email: data.email,
            name: data.name,
            surname: data.surname,
            pesel: data.pesel,
            candidateId: data.candidateId,
            birthDate: data.birthDate,

            adress: data.adress,
            fatherName: data.fatherName,
            motherName: data.motherName,
            primarySchool: data.primarySchool,
        }
        return axios.post(`/api/administration/candidates/`, json, this.authHeader()).then((resp) => {
            return resp.data;
        });
    }
    public deleteCandidate(id: number) {
        return axios.delete(`/api/administration/candidates/${id}`, this.authHeader()).then((resp) => {
            return resp.data;
        });
    }

    public downloadIdCard(userId: number) {
        return axios.get(`api/administration/candidates/${userId}/idcard`, this.blobResponseAuthHeader()).then((response) => {
            saveAs(new Blob([response.data]), `IdCard_${userId}.pdf`)
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
    private withParams(params:any ): any {
        return {
            params: params,
            headers: {
                Authorization: `Bearer ${LocalStorageService.getJwtToken()}`
            }
        };
    }
}