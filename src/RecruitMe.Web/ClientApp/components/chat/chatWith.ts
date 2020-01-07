import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../services/user.service';
import { MessageService } from '../../services/message.service';
import { IMessage } from '../../models/chat.models';
import { isToday, toLocalTime, toLocaleDateTimeString } from '../../helpers/datetime.helper';

@Component({})
export default class ChatWithComponent extends Vue {
    page: number = 1;
    person: string = '';
    readonly pageSize: number = 15;
    
    msg: string = "";

    readAll: boolean = false;
    messages: IMessage[] = [];

    loaded: boolean = false;
    fetching: boolean = false;

    userService: UserService = new UserService();
    messageService: MessageService = new MessageService();
    
    beforeMount() {
        this.person = this.$route.params.login;
        setInterval(this.getMessages, 15000);
        this.getMessages(1);
    }

    sendMessage() {
        this.messageService.sendMessage(this.person, this.msg).then(msg => {
            this.getMessages(1);
            this.msg = "";
        })
    }

    getMessages(page: number = this.page) {
        if (!this.$route.fullPath.includes("chatwith")) {
            return;
        }

        this.fetching = true;
        this.messageService.getMessages(this.person, page, this.pageSize).then(d => {
            this.processMessages(page, d.data);

            if (d.count <= this.messages.length) {
                this.readAll = true;
            }

            this.fetching = false;
        }, _ => {
            this.fetching = false;
        });
    }

    processMessages(page: number, messagesToAdd: IMessage[]) {
        // the same collections
        if (this.messages[0] && this.messages[0].timestamp == messagesToAdd[0].timestamp) {
            return;
        }

        // add and remove duplicates
        if (page == 1) {
            // the newest messages
            this.messages.push(...messagesToAdd.reverse());
        }
        else {
            // the older ones
            messagesToAdd.reverse().push(...this.messages);
            this.messages = messagesToAdd;
        }

        this.messages = this.messages.filter((v,i,a)=> {
            for (let ix = 0; ix < a.length; ix++) {
                const el = a[ix];
                if (el.isMine == v.isMine && el.message == v.message && el.timestamp == v.timestamp) {
                    return i === ix;
                } 
            }
        });
    }
    
    getDateText(timestamp: Date): string {
        const date: Date = new Date(timestamp);
        if (isToday(date)) {
            return date.toLocaleTimeString();
        }

        return toLocaleDateTimeString(date);
    }
}
