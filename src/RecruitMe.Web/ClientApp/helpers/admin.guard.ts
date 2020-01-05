import { Route } from "vue-router"
import { UserService } from "../services/user.service";

export function AdminGuard(to: Route, from: Route, next: any) {
    if (new UserService().isAdmin()) {
        next();
    }
    else {
        next("/");
    }
}