import { ApiGateway } from "../common/apiGateway";
import { LocalStorageService } from "../localStorage/localStorageService";
import { IProfileData } from "../../models/personalDataModel";
import PopupFactory from '../popupFactory';

export class PersonalDataService {
    private _apiGateway: ApiGateway = new ApiGateway();

    public getProfileData() : Promise<IProfileData | null> {
        return this._apiGateway.getProfileData().then(r => {
            LocalStorageService.setProfileData(r.data);
            return r.data; 
        }, err => {
            console.error(err);
            PopupFactory.GenericErrorPopup("" + err);
        })
    }

    public setProfileData(profileDataModel: IProfileData) : Promise<void> {
        return this._apiGateway.setProfileData(profileDataModel).then(r => {
            PopupFactory.GenericSuccessPopup("Pomyślnie zapisano dane dodatkowe");
        }, err => {
            console.error(err);
            PopupFactory.GenericErrorPopup("" + err);
        })
    }

    public examsAndStatus() {
        return this._apiGateway.examsAndStatus().then(r => {
            return r.data.exams;
        }, err => {
            console.error(err);
            PopupFactory.GenericErrorPopup("" + err);
        });
    }
}