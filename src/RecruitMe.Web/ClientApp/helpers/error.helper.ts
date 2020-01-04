export function getErrorMessage(error: any, deafultError?: string) {
    if (!error) {
        console.error("Unhandled error");
        return deafultError ? deafultError : "Niespodziewany błąd, spróbuj jeszcze raz lub skontaktuj się z administracją.";
    }
    if (error.response.status === 500) {
        console.error("Internal server error");
        console.log(error);
        return deafultError ? deafultError : "Błąd serwera.";
    }
    if (error.response.status === 400 && error.response.data) {
        console.error("Validation Error");
        console.log(error);
        if (error.response.data.error) {
            return deafultError ? deafultError : "Niespodziewany błąd, spróbuj jeszcze raz lub skontaktuj się z administracją." //Identity Server
        }
        else {
            return error.response.data.map((e: string) => `<p>${e}</p>`).join(' '); //ValidationFailedException
        }
    }
    if (error.response.status === 401) {
        console.error("Authentication Error");
        console.log(error);
        return deafultError ? deafultError : "Błąd autentykacji, spróbuj zalogować się ponownie do systemu.";
    }

    console.error("Unhandled error");
    console.log(error.response);
    return deafultError ? deafultError : "Niespodziewany błąd, spróbuj jeszcze raz lub skontaktuj się z administracją.";
}