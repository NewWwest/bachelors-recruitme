export class LocalStorageService {

    static setUserId(userId:string): void {
        localStorage.setItem('userId', userId);
    }

    static getUserId(): string | null {
        return localStorage.getItem('userId');
    }
    
    static setJwtToken(jwtToken:string): void {
        localStorage.setItem('jwtToken', jwtToken) ;
    }

    static getJwtToken(): string | null {
        return localStorage.getItem('jwtToken');
    }
    
    static setEmail(email: string): void {
        localStorage.setItem('email', email) ;
    }

    static getEmail(): string | null {
        return localStorage.getItem('email');
    }

}