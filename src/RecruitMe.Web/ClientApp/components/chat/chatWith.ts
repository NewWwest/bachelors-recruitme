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

    userService: UserService = new UserService();
    messageService: MessageService = new MessageService();

    beforeMount() {
        this.person = this.$route.params.login;
        
        //setInterval(this.getMessages, 10000);
        this.getMessages();
    }

    mounted() {
        // always on bottom
        const el = (this.$refs['mainList'].$el as Element);
        el.scrollBy(0, el.scrollHeight); 
    }

    sendMessage() {
        this.messageService.sendMessage(this.person, this.msg).then(msg => {
            this.messages.push(msg);
            this.msg = "";
        })
    }

    getMessages() {
        this.messageService.getMessages(this.person, this.page, this.pageSize).then(d => {
            if (d.page * this.pageSize >= d.count) {
                this.readAll = true;
            }

            this.processMessages(d.data);
        });
    }

    processMessages(messagesToAdd: IMessage[]) {
        //if (this.messages[0].timestamp )
        for (let i = messagesToAdd.length - 1; i >= 0; i--) {
            this.messages.push(messagesToAdd[i]);
        }
    }

    getDateText(timestamp: Date): string {
        const date: Date = new Date(timestamp);

        console.log("data", date);

        if (isToday(date)) {
            return date.toLocaleTimeString();
        }

        return toLocaleDateTimeString(date);
    }
}
