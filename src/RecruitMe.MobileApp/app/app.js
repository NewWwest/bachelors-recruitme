import Vue from "nativescript-vue";

import Home from "./components/Home";
import Router from "./router";

Vue.prototype.$router = router;
Vue.prototype.$goto = function (to, options) {
    var options = options || { 
        clearHistory: false, 
        backstackVisible: true, 
        transition: { 
            name: "slide",
            duration: 380,
            curve: "easeIn"
        }
    }
    this.$navigateTo(this.$router[to], options)
}

new Vue({

    template: `
        <Frame>
            <Home />
        </Frame>`,

    components: {
        Home
    }
}).$start();
