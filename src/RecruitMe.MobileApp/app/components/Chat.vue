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

        <GridLayout rows="auto,*">
            <NotFilledPersonalData row="0"></NotFilledPersonalData>
            <StackLayout row="1" class="pageBack-no-pad">
                <ScrollView height="85%">
                    <!-- chat thingy -->
                    <ListView for="item in messages" separatorColor="transparent" class="list">
                        <v-template>
                            <GridLayout columns="*" rows="auto" class="msg">
                                <StackLayout :class="filter(item.isMine)" orientation="horizontal" :horizontalAlignment="align(item.isMine)">
                                    <!-- <Image [visibility]="showImage(item.from)" class="authorimg" stretch="aspectFill" height="30" width="30" verticalAlignment="top" src="~/images/k1.png" col="1"></Image> -->
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

@Component({
    components: { NotFilledPersonalData }
})
export default class Chat extends Vue {
    intervalId: number = 0;

    readAll: boolean = false;
    msg: string = "";
    messages: IMessage[] = [];

    page: number = 1;
    pageSize: number = 15;
    messageService: MessageService = new MessageService();

    getNewMessages() {
        this.messageService.getMessages(this.page, this.pageSize).then(req => {
            this.processMessages(this.page, req.data);

            if (req.count <= this.messages.length) {
                this.readAll = true;
            }
        });
    }
    sendMessage() {
        this.messageService.sendMessage(this.msg).then(m => {
            this.getNewMessages();
            this.msg = "";
        }, err => {
            console.error(err);
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
        this.intervalId = setInterval(this.getNewMessages, 15000);
        this.getNewMessages();
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
    Button {
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