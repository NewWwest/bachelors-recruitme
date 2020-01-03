import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../services/user.service';
import { MessageService } from '../../services/message.service';
import { IMessage } from '../../models/chat.models';
import { isToday, toLocalTime, toLocaleDateTimeString } from '../../helpers/datetime.helper';

@Component({})
export default class ChatWithComponent extends Vue {
    page: number = 0;
    person: string = '';
    readonly pageSize: number = 15;
    
    msg: string = "";

    readAll: boolean = false;
    messages: IMessage[] = null;

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
        })
    }

    getMessages() {
        this.messages = [
            {
                isMine: true,
                message: 'cccc',
                timestamp: new Date(2020, 0, 1)
            },
            {
                isMine: false,
                message: 'bbbb',
                timestamp: new Date(2020, 0, 2)
            },
            {
                isMine: true,
                message: 'aaaaa',
                timestamp: new Date(2020, 0, 3)
            },
            {
                isMine: true,
                message: '00000',
                timestamp: new Date(2020, 0, 4)
            },
            {
                isMine: true,
                message: '-1-1-1',
                timestamp: new Date(2020, 0, 5)
            },
            {
                isMine: false,
                message: '-2-2-2',
                timestamp: new Date(2020, 0, 6)
            },
            {
                isMine: true,
                message: '-3-3-3',
                timestamp: new Date(2020, 0, 7)
            },
            
        ]

        this.messageService.getMessages(this.person, this.page, this.pageSize).then(d => {
            if (d.page * this.pageSize >= d.count) {
                this.readAll = true;
            }

            this.processMessages(d.data);
        });
    }

    processMessages(messagesToAdd: IMessage[]) {
        //if (this.messages[0].timestamp )
        for (let i = 0; i < messagesToAdd.length; i++) {
            this.messages.push(messagesToAdd[i]);
        }
    }

    getDateText(timestamp: Date): string {
        const date: Date = toLocalTime(timestamp);

        if (isToday(date)) {
            return date.toLocaleTimeString();
        }

        return toLocaleDateTimeString(date);
    }
}
