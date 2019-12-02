import { ApiGateway } from "../common/apiGateway";
import { LocalStorageService } from "../localStorage/localStorageService";
import { IPersonalData } from "../../models/personalDataModel";
import PopupFactory from '../popupFactory';

export class PersonalDataService {
    private _apiGateway: ApiGateway = new ApiGateway();

    public getPersonalData() : Promise<IPersonalData | null> {
        return this._apiGateway.getPersonalData().then(r => {
            console.log("personal data service");
            console.log(r);

            LocalStorageService.setPersonalData(r.data);
            return r.data; 
        }, err => {
            console.log(err);
            PopupFactory.GenericErrorPopup("" + err);
        })
    }
}