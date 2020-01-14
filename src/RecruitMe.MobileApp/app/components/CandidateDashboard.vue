<template>
    <Page @loaded="onNavigatingTo" class="page">
        <ActionBar class="action-bar">
            <StackLayout horizontalAlignment="left" orientation="horizontal">
                <Image src="res://mobile_menu_white" width="32" height="32"
                    @tap="onDrawerButtonTap"/>
                <Image class="dummyImage" /> <!-- dummy object to get text to center -->
                <Label class="action-bar-title" text="Ekran główny"></Label>
            </StackLayout>  
        </ActionBar>

        <GridLayout rows="auto,*">
            <NotFilledPersonalData row="0"></NotFilledPersonalData>
            <ScrollView row="1">
                <StackLayout class="pageBack">

                </StackLayout>
            </ScrollView>
        </GridLayout>
    </Page>
</template>

<script lang="ts">
import NotFilledPersonalData from '@/components/common/NotFilledPersonalData.vue'
import * as utils from '@/services/sideDrawer/utils';
import { Component, Vue } from "vue-property-decorator";
import { MessageService } from '@/services/messages/messageService';
import { LocalNotifications } from 'nativescript-local-notifications';
import { UserService } from '@/services/userService/userService';
import PopupFactory from '@/services/popupFactory';
import { Color } from 'tns-core-modules/color/color';

@Component({
    components: { NotFilledPersonalData }
})
export default class CandidateDashboard extends Vue {
    currentCount: number = 0;
    messageService: MessageService = new MessageService();
    
    onNavigatingTo() {
        setInterval(this.checkNewMessages, 30000);
        this.checkNewMessages();    
        LocalNotifications.addOnMessageReceivedCallback(notif => {
            if (UserService.isLoggedIn()) {
                this.$goto.Chat();
                this.currentCount = 0;
            }
            else {
                this.$goto.Login();
                PopupFactory.createPopup("warning", "Uwaga!", "Musisz być zalogowany/a, by przejść do chatu");
            }

            LocalNotifications.cancel(notif.id);
        })
    }

    checkNewMessages() {
        this.messageService.checkNewMessages().then(count => {
            if (count > 0 && this.currentCount != count) {
                LocalNotifications.cancelAll();
                LocalNotifications.schedule([{
                    title: `RecruitMe - masz ${count} nowych wiadomości`,
                    body: 'Kliknij, aby przejść do chatu',
                    badge: count,
                    notificationLed: new Color("#ff0000")
                }]);
            }
        })
    }
    
    onDrawerButtonTap() {
        utils.showDrawer();
    }
}
</script>

<style scoped lang="scss">
    @import '../app-common';

    .dummyImage {
        width: 24.5%;     // should be 35% - (half of head string length [circa 1% for letter])
    }
</style>