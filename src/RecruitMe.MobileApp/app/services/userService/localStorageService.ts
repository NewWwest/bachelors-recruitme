export class LocalStorageService {
    static setUserId(userId: number): void {
        if (userId == null || userId == 0)
            return;
        localStorage.setItem('userId', userId.toString());
    }

    static getUserId(): number | null {
        let data = localStorage.getItem('userId');
        return data ? parseInt(data) : null;
    }

    static setJwtToken(jwtToken: string): void {
        if (jwtToken == null || jwtToken == "")
            return;
        localStorage.setItem('jwtToken', jwtToken);
    }

    static getJwtToken(): string | null {
        return localStorage.getItem('jwtToken');
    }

    static setEmail(email: string): void {
        if (email == null || email == "")
            return;
        localStorage.setItem('email', email);
    }

    static getEmail(): string | null {
        return localStorage.getItem('email');
    }

}