export function getErrorMessage(error: any, deafultError?: string) {
    if (error.status === 500) {
        console.error("Internal server error");
        console.log(error);
        return deafultError ? deafultError : "Błąd serwera.";
    }
    if (error.status === 400 && error.data) {
        console.error("Validation Error");
        console.log(error);
        return error.data.map((e: string) => `<p>${e}</p>`).join(' ');
    }
    if (error.status === 401) {
        console.error("Authentication Error");
        console.log(error);
        return deafultError ? deafultError : "Błąd autentykacji, spróbuj zalogować się ponownie do systemu.";
    }

    console.error("Unhandled error");
    console.log(error);
    return deafultError ? deafultError : "Niespodziewany błąd, spróbuj jeszcze raz lub skontaktuj się z administracją.";
}