import store from '@/store';

export class LocalStorageService {
    static setUserId(userId: number): void {
        // if (userId == null || userId == 0)
        //     return;
        // localStorage.setItem('userId', userId.toString());

        store.commit('setUserId', userId);
    }

    static getUserId(): number {
        return store.getters.getUserId;
    }

    static setJwtToken(jwtToken: string): void {
        store.commit('setToken', jwtToken);
    }

    static getJwtToken(): string {
        return store.getters.getToken;
    }

    static setEmail(email: string): void {
        store.commit('setEmail', email);
    }

    static getEmail(): string {
        return store.getters.getEmail;
    }

}