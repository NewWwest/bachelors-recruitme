export class LocalStorageService {

    static setUserId(userId:string): void {
        if(userId ==null || userId=="")
            return;
        localStorage.setItem('userId', userId);
    }

    static getUserId(): string | null {
        return localStorage.getItem('userId');
    }
    
    static setJwtToken(jwtToken:string): void {
        if(jwtToken ==null || jwtToken=="")
            return;
        localStorage.setItem('jwtToken', jwtToken) ;
    }

    static getJwtToken(): string | null {
        return localStorage.getItem('jwtToken');
    }
    
    static setEmail(email: string): void {
        if(email ==null || email=="")
            return;
        localStorage.setItem('email', email) ;
    }

    static getEmail(): string | null {
        return localStorage.getItem('email');
    }

}