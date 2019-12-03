import store from '@/store';
import { IProfileData } from "../../models/personalDataModel";

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

    static setProfileData(profileData: IProfileData): void {
        store.commit('setProfileData', profileData);
    }

    static getProfileData(): IProfileData | null {
        return store.getters.getProfileData;
    }

}