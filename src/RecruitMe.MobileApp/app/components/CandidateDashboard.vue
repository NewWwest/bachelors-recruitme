<template>
    <Page @navigatingTo="onNavigatingTo" class="page">
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
                <GridLayout rows="150,auto,*" columns="auto,5*,6*,auto" class="pageBack">
                    <Label row="1" col="1" class="status-layout">
                        <FormattedString>
                            <Span v-if="status == 1" class="fa fa-green" text.decode="&#xf058;"/>
                            <Span v-else-if="status == 2" class="fa fa-red" text.decode="&#xf057;"/>
                            <Span v-else-if="!status" class="fa fa-yellow" text.decode="&#xf059;"/>
                        </FormattedString>
                    </Label>
                    <Label row="1" col="2" class="desc-layout" :text="paymentDescription" 
                        textWrap="true" @loaded="onDescLoaded"/>
                </GridLayout>
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
import { LocalStorageService } from '../services/localStorage/localStorageService';
import { PersonalDataService } from '../services/personalData/personalDataService';
import { EventData } from 'tns-core-modules/ui/page/page';
import { Label } from 'tns-core-modules/ui/label';
import { isAndroid } from 'tns-core-modules/platform';

@Component({
    components: { NotFilledPersonalData }
})
export default class CandidateDashboard extends Vue {
    currentCount: number = 0;
    status: number | undefined = 0;
    messageService: MessageService = new MessageService();
    personalDataService: PersonalDataService = new PersonalDataService();
    
    get paymentDescription() {
        if (this.status) {
            return this.status === 1 ? "Gratulacje! Zostałeś przyjęty! Szczegóły zostaną przekazane wkrótce drogą mailową lub telefoniczną."
                : "Niestety, ale nie jesteśmy w stanie przyjąć Ciebie do naszej szkoły.";
        }
        else return "Twój proces rekrutacji jest w trakcie. Pamiętaj by uzupełnić wszystkie potrzebne dane!";
    }

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
        });

        if (LocalStorageService.getProfileData()) {
            this.status = LocalStorageService.getProfileData().status;
        }
        else {
            this.personalDataService.getProfileData().then(profileData => {
                this.status = profileData.status;
            })
        }
    }

    checkNewMessages() {
        this.messageService.checkNewMessages().then(count => {
            if (count > 0 && this.currentCount != count) {
                LocalNotifications.cancelAll();
                LocalNotifications.schedule([{
                    title: `RecruitMe - masz ${count} nowych wiadomości`,
                    body: 'Kliknij, aby przejść do chatu',
                    badge: count,
                    notificationLed: new Color("#00ff00")
                }]);
            }
        })
    }
    
    // centers description text
    onDescLoaded(args: EventData) {
        const centerConst : number = 17; // https://developer.android.com/reference/android/view/Gravity.html#CENTER
        const lbl = args.object as Label;

        if (isAndroid) {
            lbl.android.setGravity(centerConst);
        }
    }

    onDrawerButtonTap() {
        utils.showDrawer();
    }
}
</script>

<style scoped lang="scss">
    @import '../app-common';

    .fa-red {
        color: #a60000;
        font-size: 100rem;
    }
    .fa-green {
        color: #00a600;
        font-size: 100rem;
    }
    .fa-yellow {
        color: #a6a600;
        font-size: 100rem;
    }

    .status-layout {
        background-color: #ffffff;
        border-radius: 20 0 0 20;
        text-align: center;
        vertical-align: center;
    }
    .desc-layout {
        background-color: #dddddd;
        border-radius: 0 20 20 0;
        height: 100;
        word-wrap: break-word;
        font-size: 15;
        text-align: center;
    }

    .dummyImage {
        width: 24.5%;     // should be 35% - (half of head string length [circa 1% for letter])
    }
</style>