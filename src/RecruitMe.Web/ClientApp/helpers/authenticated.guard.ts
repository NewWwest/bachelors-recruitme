import { Route } from "vue-router"
import { UserService } from "../services/user.service";

export function AuthenticatedGuard(to: Route, from: Route, next: any) {
    if (new UserService().isLoggedIn()) {
        next();
    }
    else {
        next("/account/login");
    }
}