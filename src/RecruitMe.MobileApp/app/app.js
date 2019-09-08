import Vue from "nativescript-vue";

import Login from "./components/Login";

new Vue({

    template: `
        <Frame>
            <Login />
        </Frame>`,

    components: {
        Login
    }
}).$start();
