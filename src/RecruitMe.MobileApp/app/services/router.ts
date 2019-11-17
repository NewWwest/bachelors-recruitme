import { topmost } from 'tns-core-modules/ui/frame';
import _Vue, { VueConstructor } from "vue";

// Vues
import Home from "~/components/Home.vue";
import Login from "~/components/Login.vue";
import Register from "~/components/Register.vue"
import RemindLogin from "~/components/RemindLogin.vue"
import ResetPassword from "~/components/ResetPassword.vue"

// Drawer Selected Page Service
import SelectedPageService from "@/services/sideDrawer/selectedPage.service"

export default function Router<RouterOptions>(Vue: typeof _Vue, options? : RouterOptions) {
    let goto : Goto;

    //Vue.mixin({
     //   beforeCreate() {
            goto = new Goto(options);
            Vue.prototype.$goto = goto;
    //    }
    //}) 
}

export class Goto {
    options: RouterOptions;

    constructor(_options : any) {
        this.options = _options || new RouterOptions(false, true, new Transition("slide", 380, "easeIn"));
    }

    private navigate(component : VueConstructor) {
        SelectedPageService.getInstance().updateSelectedPage(component.name);

        // it's a private object, but it's there
        topmost().currentPage.__vuePageRef__.$navigateTo(component, this.options);
    }

    /**
     * Home
     */
    public Home() {
        this.navigate(Home);
    }

    /**
     * Login
     */
    public Login() {
        this.navigate(Login);
    }

    /**
     * Register
     */
    public Register() {
        this.navigate(Register);
    }

    /**
     * ResetPassword
     */
    public ResetPassword() {
        this.navigate(ResetPassword);
    }

    /**
     * RemindLogin
     */
    public RemindLogin() {
        this.navigate(RemindLogin);
    }
} 

export class RouterOptions {
    public clearHistory: boolean;
    public backstackVisible: boolean;
    public transition: Transition;

    constructor(_clearHistory : boolean,
        _backstackVisible: boolean, _transition: Transition) {
            this.clearHistory = _clearHistory;
            this.backstackVisible = _backstackVisible;
            this.transition = _transition;
    }
}

export class Transition {
    public name: string;
    public duration: number;
    public curve: string;

    constructor(_name: string, _duration: number, _curve: string) {
        this.name = _name;
        this.duration = _duration;
        this.curve = _curve;
    }
}