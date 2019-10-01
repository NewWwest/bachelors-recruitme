import { ApiGateway } from "../api/api.gateway";
import { LoggedInUser } from "../models/loggedInUser.model";
import { LocalStorageService } from "./localStorage.service";

export class UserService {
    private _apiGateway: ApiGateway = new ApiGateway();

    public login(email: string, password: string) {
        this._apiGateway.accountLogin(email, password).then(
            (response: LoggedInUser) => {
                if (response != null) {
                    LocalStorageService.setUserId(response.id);
                    LocalStorageService.setEmail(response.email);
                    LocalStorageService.setJwtToken(response.token);
                }
            },
            (err: any) => console.error(err))
    }

    public register(email: string, password: string, cofirmPassword: string) {
        this._apiGateway.accountRegister(email, password, cofirmPassword).then(
            (response: any) => {
                if (response != null) {
                    LocalStorageService.setUserId(response.id);
                    LocalStorageService.setEmail(response.email);
                    LocalStorageService.setJwtToken(response.token);
                }
            },
            (err: any) => console.error(err))
    }
}