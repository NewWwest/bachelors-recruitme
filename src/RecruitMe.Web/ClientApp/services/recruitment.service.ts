import { ApiGateway } from "../api/api.gateway";
import { IPersonalData, IProfileData } from "../models/recruit.models";

export class RecruitmentService {
    private apiGateway: ApiGateway = new ApiGateway();

    updatePersonalData(request: IPersonalData) {
        this.apiGateway.updatePersonalData(request);
    }

    getPersonalData(): Promise<IProfileData> {
        return this.apiGateway.getPersonalData().then((d) => d.data, (err) => console.error(err));
    }

    setNewProfilePicture(fileName: string, file: any) {
        this.apiGateway.setNewProfilePicture(fileName, file);
    }
}