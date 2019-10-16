import { ApiGateway } from "../api/api.gateway";
import { IPersonalData } from "../models/recruit.models";

export class RecruitmentService {
    private apiGateway: ApiGateway = new ApiGateway();

    updatePersonalData(request: IPersonalData) {
        this.apiGateway.updatePersonalData(request);
    }

    getPersonalData(): Promise<IPersonalData> {
        return this.apiGateway.getPersonalData().then((d) => d.data, (err) => console.error(err));
    }
}