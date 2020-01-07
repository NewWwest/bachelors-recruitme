import { ApiGateway } from "../api/api.gateway";
import { IMessage, IUserThread } from "../models/chat.models";
import { LocalStorageService } from "./localStorage.service";

export class MessageService {
    private apiGateway: ApiGateway = new ApiGateway();

    public checkNewMessages(): Promise<number> {
        return this.apiGateway.checkNewMessages().then(d => {
            return d.data;
        }, err => {
            console.error(err);
            throw err;
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
        }, err => {
            console.error(err);
            throw err;
        })
    }

    public getUserThreads() {
        return this.apiGateway.getUserThreads().then(d => {
            const data: IUserThread[] = d.data;
            return data;
        }, err => {
            console.error(err);
            throw err;
        })
    }

    public sendMessage(to: string, message: string) {
        const from: number | null = LocalStorageService.getUserId();
        
        return this.apiGateway.sendMessage(from, to, message).then(d => {
            const message: IMessage = d.data;
            return message;
        }, err => {
            console.error(err);
            throw err;
        })
    }
}