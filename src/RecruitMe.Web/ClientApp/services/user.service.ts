import { ApiGateway } from "../api/api.gateway";
import { IRegistrationRequest, IAuthenticationResult, IJwtClaims, IResetPasswordRequest, ISetNewPassword, IRemindLoginRequest } from "../models/user.models";
import { LocalStorageService } from "./localStorage.service";
import { AxiosResponse } from "axios";

export class UserService {
    private _apiGateway: ApiGateway = new ApiGateway();

    public login(email: string, password: string): Promise<boolean> {
        return this._apiGateway.login(email, password).then(
            (response: AxiosResponse<IAuthenticationResult>) => {
                if (response == null || response.data == null) {
                    throw new Error();
                }

                let jwt = this.parseJwt(response.data.access_token);
                LocalStorageService.setEmail(jwt.email);
                LocalStorageService.setJwtToken(response.data.access_token);
                LocalStorageService.setUserId(jwt.userId);
                LocalStorageService.setName(jwt.name);
                LocalStorageService.setSurname(jwt.surname);

                return true;
            },
            (err: any) => {
                console.error(err);
                throw new Error();
            })
    }

    public register(registrationModel: IRegistrationRequest): Promise<number> {
        return this._apiGateway.register(registrationModel).then(
            (response: AxiosResponse<number>) => {
                if (response == null || response.data == null) {
                    throw new Error();
                }

            }, (err: any) => {
                console.error(err);
                throw err.response;
            });
    }

    public resetPassword(resetmodel: IResetPasswordRequest): Promise<void> {
        return this._apiGateway.resetPassword(resetmodel).then(
            (response: AxiosResponse<number>) => {
                
            }, (err: any) => {
                console.error(err);
                throw err;;
            });
    }

    public setNewPassword(resetModel: ISetNewPassword): Promise<void> {
        return this._apiGateway.setNewPassword(resetModel).then(
            (response: AxiosResponse<number>) => {

            }, (err: any) => {
                console.error(err);
                throw err;;
            });
    }

    public remindLogin(remindModel: IRemindLoginRequest): Promise<void> {
        return this._apiGateway.remindLogin(remindModel).then(
            (response: AxiosResponse<string>) => {

            }, (err: any) => {
                console.error(err);
                throw err;;
            });
    }

    public logout(): void {
        LocalStorageService.resetEmail();
        LocalStorageService.resetJwtToken();
        LocalStorageService.resetUserId();
        LocalStorageService.resetName();
        LocalStorageService.resetSurname();
    }

    public getDisplayName(): string {
        let name = LocalStorageService.getName();
        let surname = LocalStorageService.getSurname();
        //User should have both or neither
        return name ? name + " " + surname : "";
    }

    public isLoggedIn(): boolean {
        return LocalStorageService.getUserId() != null &&
            LocalStorageService.getEmail() != '' &&
            LocalStorageService.getJwtToken() != '';
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