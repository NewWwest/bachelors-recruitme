export interface IAuthenticationResult {
    access_token: string;
    expires_in: number;
    token_type: string;
    scope: string;
}
export interface IRegistrationRequest {
    email: string;
    password: string;
    confirmPassword: string;
    name: string;
    surname: string;
    pesel: string | null;
    noPesel: boolean;
}
export interface IJwtClaims {
    email: string;
    userId: number;
    name: string;
    surname: string
}

