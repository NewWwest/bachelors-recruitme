import { ApiGateway } from "../common/apiGateway";
import { LocalStorageService } from "./localStorageService";
import { AxiosResponse } from "axios";
import { IAuthenticationResult, IRegistrationRequest,
    IJwtClaims, IRemindLoginRequest,
    IResetPasswordRequest, ISetNewPassword } from "../../models/userFormModel";
import PopupFactory from '../popupFactory';

export class UserService {
    private _apiGateway: ApiGateway = new ApiGateway();

    public login(email: string, password: string) {
        return this._apiGateway.login(email, password).then(
            (response: AxiosResponse<IAuthenticationResult>) => {
                if (response != null && response.data != null) {
                    let jwt = this.parseJwt(response.data.access_token);
                    LocalStorageService.setEmail(jwt.email);
                    LocalStorageService.setJwtToken(response.data.access_token);
                    LocalStorageService.setUserId(jwt.userId);
                }
            },
            (err: any) => {
                console.error(err);

                PopupFactory.GenericErrorPopup("" + err);
            })
    }

    public register(registrationModel: IRegistrationRequest): Promise<number> {
        return this._apiGateway.register(registrationModel).then(
            (response: AxiosResponse<number>) => {
                if (response != null && response.data != null) {
                    console.log(`Registration succesfull, internal ID: ${response.data}`)
                }
            },(err: any) => {
                console.error(err);

                PopupFactory.GenericErrorPopup("" + err);
            });
    }

    public resetPassword(resetmodel: IResetPasswordRequest): Promise<void> {
        return this._apiGateway.resetPassword(resetmodel).then(
            (response: AxiosResponse<number>) => {
                PopupFactory.GenericSuccessPopup("Na adres e-mail podany przy rejestracji została wyslana wiadomosc z linkiem do zmiany hasla");
            }, (err: any) => {
                console.error(err);

                PopupFactory.GenericErrorPopup("" + err);
            });
    }

    public setNewPassword(resetModel: ISetNewPassword): Promise<void> {
        return this._apiGateway.setNewPassword(resetModel).then(
            (response: AxiosResponse<number>) => {

            }, (err: any) => {
                console.error(err);

                PopupFactory.GenericErrorPopup("" + err);
            });
    }

    public remindLogin(remindModel: IRemindLoginRequest): Promise<void> {
        return this._apiGateway.remindLogin(remindModel).then(
            (response: AxiosResponse<string>) => {
                PopupFactory.GenericSuccessPopup("Na adres e-mail podany przy rejestracji został wysłany Twój login");
            }, (err: any) => {
                console.error(err);

                const messageText = remindModel.pesel ? "Niepoprawny adres e-mail i/lub numer PESEL" :
                    "Niepoprawny adres e-mail, imię i/lub nazwisko użytkownika";

                PopupFactory.createPopup("error", "Nie można przypomnieć loginu!",
                    messageText);
            });
    }

    public static isLoggedIn(): boolean {
        return LocalStorageService.getUserId() ? true : false &&
            LocalStorageService.getEmail() ? true : false  &&
            LocalStorageService.getJwtToken() ? true : false;
    }

    private parseJwt(token: string): IJwtClaims {
        try {
            var base64Url = token.split('.')[1];
            var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
            var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
                return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
            }).join(''));

            return JSON.parse(jsonPayload);
        }
        catch (e) {
            console.error(e);
            throw e;
        }
    }

}