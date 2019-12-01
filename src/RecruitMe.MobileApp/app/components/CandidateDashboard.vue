<template>
    <Page class="page">
        <ActionBar class="action-bar">
                <StackLayout horizontalAlignment="left" orientation="horizontal">
                    <Image src="res://mobile_menu_white" width="32" height="32"
                     @tap="onDrawerButtonTap"/>
                    <Image class="dummyImage" /> <!-- dummy object to get text to center -->
                    <Label class="action-bar-title" text="Home"></Label>
                </StackLayout>  
        </ActionBar>

        <ScrollView>
            <StackLayout>
                <StackLayout class="detailsInfoBar">

                </StackLayout>
                <StackLayout class="pageBack">
                
                </StackLayout>
            </StackLayout>
        </ScrollView>
    </Page>
</template>

<script lang="ts">
import * as utils from '@/services/sideDrawer/utils';
import { Component, Vue } from "vue-property-decorator";

@Component
export default class CandidateDashboard extends Vue {
    onDrawerButtonTap() {
        utils.showDrawer();
    }
}
</script>

<style scoped lang="scss">
    @import '../app-common';

    .dummyImage {
        width: 33%;     // should be 35% - (half of head string length [circa 1% for letter])
    }
</style>