import { ApiGateway } from "../common/apiGateway";
import PopupFactory from '../popupFactory';

export class PaymentService {
    private _apiGateway: ApiGateway = new ApiGateway();

    public getPaymentLink() {
        return this._apiGateway.getPaymentLink().then(r => {
            return r.data;
        }, err => {
            console.error(err);
            PopupFactory.GenericErrorPopup(
                "Nie mogliśmy wygenerować linku płatności - prosimy o zgłoszenie sytuacji administratorowi");
        })
    }
}
