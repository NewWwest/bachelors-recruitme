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
    birthDate: Date;
}
export interface IJwtClaims {
    email: string;
    userId: number;
    name: string;
    surname: string;
    pesel: string;
}
export interface IResetPasswordRequest {
    login: string
}
export interface ISetNewPassword {
    password: string;
    confirmPassword: string;
    token: string;
}
export interface IRemindLoginRequest {
    email: string;
    name: string | null;
    surname: string | null;
    pesel: string | null;
}
