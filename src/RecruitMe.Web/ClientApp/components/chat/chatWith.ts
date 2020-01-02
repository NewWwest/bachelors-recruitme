import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../services/user.service';
import { MessageService } from '../../services/message.service';
import { IMessage } from '../../models/chat.models';

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

    sendMessage() {
        this.messageService.sendMessage(this.person, this.msg).then(msg => {
            this.messages.push(msg);
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
        for (let i = 0; i < messagesToAdd.length; i++) {
            this.messages.push(messagesToAdd[i]);
        }
    }
}
