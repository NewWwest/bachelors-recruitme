<template>
    <Page class="page">
        <ActionBar class="action-bar">
            <StackLayout horizontalAlignment="left" orientation="horizontal">
                <Image src="res://mobile_menu_white" width="32" height="32"
                    @tap="onDrawerButtonTap"/>
                <Image class="dummyImage" /> <!-- dummy object to get text to center -->
                <Label class="action-bar-title" text="Płatności"></Label>
            </StackLayout>  
        </ActionBar>

        <GridLayout rows="auto,*,auto,auto">
            <NotFilledPersonalData row="0"></NotFilledPersonalData>
            <ScrollView row="1">
                <GridLayout rows="150,auto,*" columns="auto,5*,6*,auto" class="pageBack">
                    <Label row="1" col="1" class="status-layout">
                        <FormattedString>
                            <Span v-if="isPaymentMade" class="fa fa-green text-center" text.decode="&#xf058;"/>
                            <Span v-else class="fa fa-red text-center" text.decode="&#xf057;"/>
                        </FormattedString>
                    </Label>
                    <Label row="1" col="2" class="desc-layout text-center" :text="paymentDescription" textWrap="true"/>
                </GridLayout>
            </ScrollView>
            <StackLayout row="2" class="pageBack">
                <Button text="Przejdź do płatności" class="my-button paymentButton" @tap="goToPayment()"/>
            </StackLayout>
        </GridLayout>
    </Page>
</template>

<script lang="ts">
import NotFilledPersonalData from '@/components/common/NotFilledPersonalData.vue'
import { PaymentService } from '../services/paymentService/paymentService'
import * as utils from '@/services/sideDrawer/utils';
import { Component, Vue } from "vue-property-decorator";
import { openUrl } from "tns-core-modules/utils/utils";

@Component({
    components: { NotFilledPersonalData }
})
export default class Payments extends Vue {
    fetching: boolean = false;
    isPaymentMade: boolean = false;
    paymentService: PaymentService = new PaymentService();
    
    get paymentDescription() {
        return this.isPaymentMade ? 'Płatność została zaksięgowana w naszym systemie' 
         : 'Płatność nie została zaksięgowana w naszym systemie';
    }

    goToPayment() {
        this.paymentService.getPaymentLink().then(url => {
            openUrl(url);
        });
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

    .status-layout {
        background-color: #ffffff;
        border-radius: 20 0 0 20;
        text-align: center;
        vertical-align: middle;
    }
    .desc-layout {
        background-color: #dddddd;
        border-radius: 0 20 20 0;
        height: 100;
        word-wrap: break-word;
        text-align: center;
        padding-top: 18;
        font-size: 15;
    }

    .paymentButton {
        margin-bottom: 40;
    }

    .dummyImage {
        width: 28%;     // should be 35% - (half of head string length [circa 1% for letter])
    }
</style>