import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { MessageService } from '../../services/message.service';
import { IUserThread } from '../../models/chat.models';

@Component({})
export default class UserListComponent extends Vue {
    userThreads: IUserThread[] = [];
    messageService: MessageService = new MessageService();

    beforeMount() {
        setInterval(this.getThreads, 15000);
        this.getThreads();
    }

    getThreads() {
        if (!this.$route.fullPath.includes("chat/threads")) {
            return;
        }

        this.messageService.getUserThreads().then(threads => {
            // add and remove duplicates
            this.userThreads.push(...threads);
            this.userThreads = this.userThreads.filter((v,i,a) => {
                for (let ix = 0; ix < a.length; ix++) {
                    const el = a[ix];
                    if (el.userId == v.userId) {
                        if (i !== ix) {
                            a[i].newMessagesCount = v.newMessagesCount;
                            return false;
                        }
                        return true;
                    } 
                }
            })
        })
    }

    onClick(thread: IUserThread) {
        this.$router.push(`/chatwith/${thread.userId}`);
    }

    getColorForCount(count: number) {
        if (count == 0) {
            return "#AAAAAA";
        }
        else if (count < 10) {
            let yellow: number = 255;

            return `rgb(255,${255 - (count - 1)/8*255},0)`;
        }
        else {
            return '#6102A6';
        }
    }
    getTextColorForCount(count: number) {
        if (count >= 0 && count <= 5) {
            return "black";
        }
        else return "white";
    }
}