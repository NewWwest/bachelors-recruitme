import { ApiGateway } from "../common/apiGateway";
import { IMessage } from "../../models/chatModel";
import PopupFactory from '../popupFactory';

export class MessageService {
    readonly toPerson: string = 'admin';
    private _apiGateway: ApiGateway = new ApiGateway();

    public getMessages(page: number, pageSize: number) {
        return this._apiGateway.getMessages(this.toPerson, page, pageSize).then(r => {
            const count = r.totalCount;
            const page = r.page;
            const data : IMessage[] = r.data;

            return {
                count: count,
                page: page,
                data: data
            }
        }, err => {
            console.error(err);
            PopupFactory.GenericErrorPopup("" + err);
            throw err;
        });
    }

    public sendMessage(text: string): Promise<IMessage> {
        return this._apiGateway.sendMessage(this.toPerson, text).then(r => {
            const message: IMessage = r;
            return message;
        }, err => {
            console.error(err);
            PopupFactory.GenericErrorPopup("" + err);
            throw err;
        });
    }

    public checkNewMessages() {
        return this._apiGateway.checkNewMessages().then(r => {
            return r.data;
        }, err => {
            console.error(err);
            PopupFactory.GenericErrorPopup("" + err);
        });
    }
}