<template>
    <Page @unloaded="onNavigatingFrom" @loaded="onNavigatingTo" class="page">
        <ActionBar class="action-bar">
            <StackLayout horizontalAlignment="left" orientation="horizontal">
                <Image src="res://mobile_menu_white" width="32" height="32"
                    @tap="onDrawerButtonTap"/>
                <Image class="dummyImage" /> <!-- dummy object to get text to center -->
                <Label class="action-bar-title" text="Chat"></Label>
            </StackLayout>  
        </ActionBar>

        <GridLayout rows="auto,auto,*">
            <NotFilledPersonalData row="0"></NotFilledPersonalData>
            <StackLayout row="1" class="pageBack-no-pad">
                <Button v-if="!readAll" @tap="getMessages(page+1)" text="Pobierz więcej wiadomości"/>
            </StackLayout>
            <StackLayout row="2" class="pageBack-no-pad">
                <ScrollView height="85%">
                        <!-- chat thingy -->
                        <ListView for="item in messages" separatorColor="transparent" class="list">
                            <v-template>
                                <GridLayout columns="*" rows="auto" class="msg">
                                    <StackLayout :class="filter(item.isMine)" orientation="horizontal" :horizontalAlignment="align(item.isMine)">
                                        <Label :text="item.message" class="msg_text" textWrap="true" verticalAlignment="top"></Label>
                                    </StackLayout>
                                </GridLayout>
                            </v-template>
                        </ListView>
                        <!-- end of chat thingy -->
                </ScrollView>
                <StackLayout height="15%">
                    <GridLayout columns="*,auto">
                        <TextField class="chatTextField" row="0" col="0" v-model="msg"></TextField>
                        <Button class="chatBtn" row="0" col="1" @tap="sendMessage()">
                            <FormattedString>
                                <Span class="fa" text.decode="&#xf124; "/>
                            </FormattedString>
                        </Button>
                    </GridLayout>
                </StackLayout>
            </StackLayout>
        </GridLayout>
    </Page>
</template>

<script lang="ts">
import NotFilledPersonalData from '@/components/common/NotFilledPersonalData.vue'
import { MessageService } from '../services/messages/messageService'
import * as utils from '@/services/sideDrawer/utils';
import { Component, Vue } from "vue-property-decorator";
import { setInterval, clearInterval } from 'tns-core-modules/timer';
import { IMessage } from '../models/chatModel';
import { LocalNotifications } from "nativescript-local-notifications";

@Component({
    components: { NotFilledPersonalData }
})
export default class Chat extends Vue {
    intervalId: number = 0;
    firstCall: boolean = true;

    readAll: boolean = false;
    msg: string = "";
    messages: IMessage[] = [];

    page: number = 1;
    pageSize: number = 15;
    messageService: MessageService = new MessageService();

    getMessages(page: number = 1) {
        this.messageService.getMessages(page, this.pageSize).then(req => {
            this.processMessages(page, req.data);

            this.page = page > 1 ? page : this.page;
            if (req.count <= this.messages.length) {
                this.readAll = true;
            }

            if (this.firstCall) {
                LocalNotifications.cancelAll();
                this.firstCall = false;
            }
        });
    }
    sendMessage() {
        this.messageService.sendMessage(this.msg).then(m => {
            this.getMessages();
            this.msg = "";
        }, err => {
        });
    }
    processMessages(page: number, messagesToAdd: IMessage[]) {
        // the same collections
        if (this.messages[0] && this.messages[0].timestamp == messagesToAdd[0].timestamp) {
            return;
        }
        else if (this.messages.length === 0) {
            this.messages.push(...messagesToAdd.reverse());
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

    filter(isMine: boolean) {
        return isMine ? "mineMsg" : "otherMsg";
    }
    align(isMine: boolean) {
        return isMine ? "right" : "left";
    }

    onNavigatingTo() {
        this.intervalId = setInterval(this.getMessages, 15000);
        this.getMessages();
    }
    onNavigatingFrom() {
        clearInterval(this.intervalId);
    }

    onDrawerButtonTap() {
        utils.showDrawer();
    }
}
</script>

<style scoped lang="scss">
    @import '../app-common';
    ListView {
        separator-color: #ffffff00;
    }
    .dummyImage {
        width: 32%;     // should be 35% - (half of head string length [circa 1% for letter])
    }
    .fa {
        font-size: 23;
    }
    .chatTextField {
        padding: 5;
        margin: 0 5; 
        background-color: lightgray; 
        border-radius: 4;
        height: 40;
    }
    .chatBtn {
        padding: 5;
        margin: 5;
        background-color: dodgerblue;
        color: white;
        height: 40;
        width: 40;
        border-radius: 20;
    }
    .msg {
        font-size: 14;
        padding: 5;
    }
    .mineMsg .msg_text {
        background-color: #30A9FF;
        color: white;
        padding: 8;
        margin-right: 10;
        border-radius: 5;
    }
    .otherMsg .msg_text {
        background-color: #e0e0e0;
        color: #333;
        padding: 7;
        border-radius: 5;
        margin-right: 40;
    }
</style>