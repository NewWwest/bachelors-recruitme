export class ApiGateway {
    public accountLogin(email:string, password:string):any{
        const requestOpt = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: { email, password }
        };

        return new Promise(() => {});
    }
}