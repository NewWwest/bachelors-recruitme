import { ApiGateway } from "../api/api.gateway";
import { IMessage, IUserThread } from "../models/chat.models";

export class MessageService {
    private apiGateway: ApiGateway = new ApiGateway();

    public checkNewMessages(): Promise<number> {
        return this.apiGateway.checkNewMessages().then(d => {
            return d.data;
        })
    }

    public getMessages(person: string, page: number, pageSize: number) {
        return this.apiGateway.getMessages(person, page, pageSize).then(d => {
            const count = d.data.totalCount;
            const page = d.data.page;
            const data : IMessage[] = d.data.data;

            return {
                count: count,
                page: page,
                data: data
            }
        })
    }

    public getUserThreads() {
        return this.apiGateway.getUserThreads().then(d => {
            const data: IUserThread[] = d.data;

            return data;
        })
    }

    public sendMessage(person: string, message: string) {
        return this.apiGateway.sendMessage(person, message).then(d => {
            const message: IMessage = d.data;
            return message;
        }, err => {
            console.log(err);
            throw err;
        })
    }
}