<template>
    <Page @unloaded="onNavigatingFrom" class="page">
        <ActionBar class="action-bar">
            <StackLayout horizontalAlignment="left" orientation="horizontal">
                <Image src="res://mobile_menu_white" width="32" height="32"
                    @tap="onDrawerButtonTap"/>
                <Image class="dummyImage" /> <!-- dummy object to get text to center -->
                <Label class="action-bar-title" text="Chat"></Label>
            </StackLayout>  
        </ActionBar>

        <GridLayout rows="auto,*,auto,auto">
            <NotFilledPersonalData row="0"></NotFilledPersonalData>
            <ScrollView row="1">
                <GridLayout rows="150,auto,*" columns="auto,5*,6*,auto" class="pageBack">

                </GridLayout>
            </ScrollView>
        </GridLayout>
    </Page>
</template>

<script lang="ts">
import NotFilledPersonalData from '@/components/common/NotFilledPersonalData.vue'
import { MessageService } from '../services/messages/messageService'
import * as utils from '@/services/sideDrawer/utils';
import { Component, Vue } from "vue-property-decorator";
import { setInterval, clearInterval } from 'tns-core-modules/timer';

@Component({
    components: { NotFilledPersonalData }
})
export default class Payments extends Vue {
    intervalId: number = 0;

    page: number = 1;
    pageSize: number = 15;
    messageService: MessageService = new MessageService();
    
    beforeMount() {
        this.intervalId = setInterval(this.getNewMessages, 15000);
        this.getNewMessages();
    }

    getNewMessages() {
        this.messageService.getMessages(this.page, this.pageSize).then(messages => {
            
        })
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
    .dummyImage {
        width: 28%;     // should be 35% - (half of head string length [circa 1% for letter])
    }
</style>