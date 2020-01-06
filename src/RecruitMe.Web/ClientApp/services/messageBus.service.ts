import Vue from "vue";

export class MessageBusService {
    static internalBus: Vue = new Vue();

    static onError(handler: any) {
        this.internalBus.$on("app-error", handler);
    }
    static emitError(data: string) {
        this.internalBus.$emit("app-error", data);
    }

    static onUserChanged(handler: any) {
        this.internalBus.$on('user-logged-in', handler);
    }
    static emitUserChanged(data: boolean) {
        this.internalBus.$emit('user-logged-in', data);
    }
}