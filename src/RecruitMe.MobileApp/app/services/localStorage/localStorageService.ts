import store from '@/store';
import { IPersonalData } from "../../models/personalDataModel";

export class LocalStorageService {
    static setUserId(userId: number): void {
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

    static setFullname(fullname: string): void {
        store.commit('setFullname', fullname);
    }

    static getFullname(): string {
        return store.getters.getFullname;
    }

    static setPersonalData(personalData: IPersonalData): void {
        store.commit('setPersonalData', personalData);
    }

    static getPersonalData(): IPersonalData | null {
        return store.getters.getPersonalData;
    }

}