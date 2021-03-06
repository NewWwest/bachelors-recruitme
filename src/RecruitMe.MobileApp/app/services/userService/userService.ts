import { ApiGateway } from "../common/apiGateway";
import { LocalStorageService } from "../localStorage/localStorageService";
import { AxiosResponse } from "axios";
import { IAuthenticationResult, IRegistrationRequest,
    IJwtClaims, IRemindLoginRequest, IResetPasswordRequest } from "../../models/userFormModel";
import PopupFactory from '../popupFactory';
import { PersonalDataService } from '../personalData/personalDataService';

export class UserService {
    private _apiGateway: ApiGateway = new ApiGateway();

    public login(email: string, password: string) {
        return this._apiGateway.login(email, password).then(
            async (response: AxiosResponse<IAuthenticationResult>) => {
                if (response != null && response.data != null) {
                    let jwt = this.parseJwt(response.data.access_token);

                    LocalStorageService.setEmail(jwt.email);
                    LocalStorageService.setJwtToken(response.data.access_token);
                    LocalStorageService.setUserId(jwt.userId);
                    LocalStorageService.setFullname(jwt.name + ' ' + jwt.surname);

                    const service: PersonalDataService = new PersonalDataService();
                    await service.getProfileData();
                }
            })
    }

    public register(registrationModel: IRegistrationRequest): Promise<void> {
        return this._apiGateway.register(registrationModel).then(
            (response: AxiosResponse<number>) => {
                if (response != null && response.data != null) {
                    console.log(`Registration succesfull, internal ID: ${response.data}`);
                    PopupFactory.GenericSuccessPopup("Na adres e-mail podany w rejestracji został wysłany mail z informacjami");
                }
            }, err => {
                console.error(err);
                PopupFactory.GenericErrorPopup("" + err);
            });
    }

    public resetPassword(resetmodel: IResetPasswordRequest): Promise<void> {
        return this._apiGateway.resetPassword(resetmodel).then(
            (response: AxiosResponse<number>) => {
                PopupFactory.GenericSuccessPopup("Na adres e-mail podany przy rejestracji została wyslana wiadomosc z linkiem do zmiany hasla");
            }, err => {
                console.error(err);

                PopupFactory.GenericErrorPopup("" + err);
            });
    }

    public remindLogin(remindModel: IRemindLoginRequest): Promise<void> {
        return this._apiGateway.remindLogin(remindModel).then(
            (response: AxiosResponse<string>) => {
                PopupFactory.GenericSuccessPopup("Na adres e-mail podany przy rejestracji został wysłany Twój login");
            }, err => {
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
            var jsonPayload = decodeURIComponent(this.atobNS(base64).split('').map(function (c) {
                return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
            }).join(''));

            return JSON.parse(jsonPayload);
        }
        catch (e) {
            console.error(e);
            throw e;
        }
    }

    private atobNS(str: string): string {
        const data = android.util.Base64.decode(str, android.util.Base64.NO_WRAP);
        const androidString =  new java.lang.String(data,  java.nio.charset.StandardCharsets.UTF_8 );

        return '' + androidString;
    }
}