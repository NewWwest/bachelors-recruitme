import { ApiGateway } from "../api/api.gateway";

export class PaymentsService {
    private apiGateway: ApiGateway = new ApiGateway();

    public makePayment() : Promise<string> {
        return this.apiGateway.makePayment().then(
            (d) => d.data,
            (err) => console.error(err)
        );
    } 
}