import { ApiGateway } from "../api/api.gateway";
import { IPersonalData, IProfileData, IExamsAndStatusDto } from "../models/recruit.models";

export class RecruitmentService {
    private apiGateway: ApiGateway = new ApiGateway();

    updatePersonalData(request: IPersonalData): Promise<IProfileData> {
        return this.apiGateway.updatePersonalData(request).then(
            (d) => d.data,
            (err) => {
                console.error(err);
                throw err;
            });
        );
    }

    getProfile(): Promise<IProfileData> {
        return this.apiGateway.getProfile().then(
            (d) => d.data,
            (err) => {
                console.error(err);
                throw err;
            });
        );
    }

    setNewProfilePicture(fileName: string, file: any): Promise<IProfileData> {
        return this.apiGateway.setNewProfilePicture(fileName, file).then(
            r => this.getProfile(),
            (err) => {
                console.error(err);
                throw err;
            });
    }

    uploadDocument(filename: string, file: any): Promise<IProfileData> {
        return this.apiGateway.uploadDocument(filename, file).then(
            r => this.getProfile(),
            (err) => {
                console.error(err);
                throw err;
            });
    }

    deleteDocument(fileId: number): Promise<IProfileData> {
        return this.apiGateway.deleteDocument(fileId).then(
            r => this.getProfile(),
            (err) => {
                console.error(err);
                throw err;
            });
    }

    examsAndStatus(): Promise<IExamsAndStatusDto> {
        return this.apiGateway.examsAndStatus().then(
            r => {
                return r.data;
            },
            (err) => {
                console.error(err);
                throw err;
            });
    }
}