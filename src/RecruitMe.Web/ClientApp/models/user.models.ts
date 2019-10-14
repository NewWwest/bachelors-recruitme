export interface LoggedInUser {
    id: string;
    email: string;
    token: string;
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