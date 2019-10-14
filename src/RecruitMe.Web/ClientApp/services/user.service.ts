import { ApiGateway } from "../api/api.gateway";
import { LoggedInUser, IRegistrationRequest } from "../models/user.models";
import { LocalStorageService } from "./localStorage.service";
import { AxiosResponse } from "axios";

export class UserService {
    private _apiGateway: ApiGateway = new ApiGateway();

    public login(email: string, password: string) {
        this._apiGateway.login(email, password).then(
            (response: AxiosResponse<LoggedInUser>) => {
                console.log(response);
                if (response != null && response.data != null) {
                    LocalStorageService.setUserId(response.data.id);
                    LocalStorageService.setEmail(response.data.email);
                    LocalStorageService.setJwtToken(response.data.token);
                }
            },
            (err: any) => console.error(err))
    }

    public register(registrationModel: IRegistrationRequest): Promise<LoggedInUser> {
        return this._apiGateway.register(registrationModel).then(
            (response: AxiosResponse<LoggedInUser>) => {
                if (response != null && response.data != null) {
                    LocalStorageService.setUserId(response.data.id);
                    LocalStorageService.setEmail(response.data.email);
                    LocalStorageService.setJwtToken(response.data.token);
                    return response.data;
                }
            },(err: any) => {
                console.error(err);
                return err;;
            });
    }

    public isLoggedIn(): boolean {
        return LocalStorageService.getUserId() != null &&
            LocalStorageService.getEmail() != null &&
            LocalStorageService.getJwtToken() != null;
    }
}