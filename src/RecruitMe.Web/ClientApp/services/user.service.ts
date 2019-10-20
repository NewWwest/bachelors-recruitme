import { ApiGateway } from "../api/api.gateway";
import { IRegistrationRequest, IAuthenticationResult, IJwtClaims } from "../models/user.models";
import { LocalStorageService } from "./localStorage.service";
import { AxiosResponse } from "axios";

export class UserService {
    private _apiGateway: ApiGateway = new ApiGateway();

    public login(email: string, password: string): Promise<boolean> {
        return this._apiGateway.login(email, password).then(
            (response: AxiosResponse<IAuthenticationResult>) => {
                if (response == null || response.data == null) {
                    return false;
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
                return false;
            })
    }

    public register(registrationModel: IRegistrationRequest): Promise<number> {
        return this._apiGateway.register(registrationModel).then(
            (response: AxiosResponse<number>) => {
                if (response != null && response.data != null) {
                    alert(`Registration succesfull, internal ID: ${response.data}`)
                }
            },(err: any) => {
                console.error(err);
                return err;;
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
        return name ? name + surname : "";
    }

    public isLoggedIn(): boolean {
        console.log(LocalStorageService.getUserId())
        console.log(LocalStorageService.getEmail())
        console.log(LocalStorageService.getJwtToken())
        return LocalStorageService.getUserId() != null &&
            LocalStorageService.getEmail() != null &&
            LocalStorageService.getJwtToken() != null;
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