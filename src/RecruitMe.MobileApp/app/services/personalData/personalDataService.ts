import { ApiGateway } from "../common/apiGateway";
import { LocalStorageService } from "../localStorage/localStorageService";
import { IProfileData } from "../../models/personalDataModel";
import PopupFactory from '../popupFactory';

export class PersonalDataService {
    private _apiGateway: ApiGateway = new ApiGateway();

    public getProfileData() : Promise<IProfileData | null> {
        return this._apiGateway.getProfileData().then(r => {
            console.log("personal data service");
            console.log(r);

            LocalStorageService.setProfileData(r.data);
            return r.data; 
        }, err => {
            console.log(err);
            PopupFactory.GenericErrorPopup("" + err);
        })
    }

    public setProfileData(profileDataModel: IProfileData) : Promise<void> {
        return this._apiGateway.setProfileData(profileDataModel).then(r => {
            console.log(r);
            PopupFactory.GenericSuccessPopup("Pomyślnie zapisano dane dodatkowe");
        }, err => {
            console.log(err);
            PopupFactory.GenericErrorPopup("" + err);
        })
    }
}