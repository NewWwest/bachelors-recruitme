import { ApiGateway } from "../common/apiGateway";
import { LocalStorageService } from "./localStorageService";
import { AxiosResponse } from "axios";
import { IAuthenticationResult, IRegistrationRequest, IJwtClaims } from "../../models/userFormModel";

export class UserService {
    private _apiGateway: ApiGateway = new ApiGateway();

    public login(email: string, password: string) {
        this._apiGateway.login(email, password).then(
            (response: AxiosResponse<IAuthenticationResult>) => {
                if (response != null && response.data != null) {
                    let jwt = this.parseJwt(response.data.access_token);
                    LocalStorageService.setEmail(jwt.email);
                    LocalStorageService.setJwtToken(response.data.access_token);
                    LocalStorageService.setUserId(jwt.userId);
                }
            },
            (err: any) => console.error(err))
    }

    public register(registrationModel: IRegistrationRequest): Promise<number> {
        return this._apiGateway.register(registrationModel).then(
            (response: AxiosResponse<number>) => {
                if (response != null && response.data != null) {
                    console.log(`Registration succesfull, internal ID: ${response.data}`)
                }
            },(err: any) => {
                console.error(err);
                return err;
            });
    }

    public isLoggedIn(): boolean {
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