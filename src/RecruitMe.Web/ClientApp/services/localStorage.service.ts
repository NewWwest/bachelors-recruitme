export class LocalStorageService {
    //JwtClaims.ClaimId
    static setUserId(userId: number): void {
        if (userId == null || userId == 0)
            return;
        localStorage.setItem('userId', userId.toString());
    }
    static getUserId(): number | null {
        let data = localStorage.getItem('userId');
        return data ? parseInt(data) : null;
    }
    static resetUserId(): void {
        localStorage.setItem('userId', '');
    }

    //Jwt
    static setJwtToken(jwtToken: string): void {
        if (jwtToken == null || jwtToken == "")
            return;
        localStorage.setItem('jwtToken', jwtToken);
    }
    static getJwtToken(): string | null {
        return localStorage.getItem('jwtToken');
    }
    static resetJwtToken(): void {
        localStorage.setItem('jwtToken', '');
    }

    //JwtClaims.ClaimEmail
    static setEmail(email: string): void {
        if (email == null || email == "")
            return;
        localStorage.setItem('email', email);
    }
    static getEmail(): string | null {
        return localStorage.getItem('email');
    }
    static resetEmail(): void {
        localStorage.setItem('email', '');
    }

    //JwtClaims.ClaimName
    static setName(name: string): void {
        if (name == null || name == "")
            return;
        localStorage.setItem('name', name);
    }
    static getName(): string | null {
        return localStorage.getItem('name');
    }
    static resetName(): void {
        localStorage.setItem('name', '');
    }

    //JwtClaims.ClaimSurname
    static setSurname(surname: string): void {
        if (surname == null || surname == "")
            return;
        localStorage.setItem('surname', surname);
    }
    static getSurname(): string | null {
        return localStorage.getItem('email');
    }
    static resetSurname(): void {
        localStorage.setItem('surname', '');
    }
    
    //JwtClaims.IsAdmin
    static setIsAdmin(admin: boolean): void {
        localStorage.setItem('isadmin', admin ? "1" : "0");
    }
    static getIsAdmin(): boolean {
        return localStorage.getItem('isadmin') == "1";
    }
    static resetIsAdmin(): void {
        localStorage.setItem('isadmin', '0');
    }
}