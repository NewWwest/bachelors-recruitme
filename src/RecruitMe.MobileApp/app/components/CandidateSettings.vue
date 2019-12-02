<template>
    <Page class="page">
        <ActionBar class="action-bar">
            <StackLayout horizontalAlignment="left" orientation="horizontal">
                <Image src="res://mobile_menu_white" width="32" height="32"
                    @tap="onDrawerButtonTap"/>
                <Image class="dummyImage" /> <!-- dummy object to get text to center -->
                <Label class="action-bar-title" text="Ustawienia"></Label>
            </StackLayout> 
        </ActionBar>

        <ScrollView>
            <StackLayout class="pageBack">
            
            </StackLayout>
        </ScrollView>
    </Page>
</template>

<script lang="ts">
import * as utils from '@/services/sideDrawer/utils';
import { Component, Vue } from "vue-property-decorator";

@Component
export default class CandidateSettings extends Vue {
    onDrawerButtonTap() {
        utils.showDrawer();
    }
}
</script>

<style scoped lang="scss">
    @import '../app-common';

    .dummyImage {
        width: 27.5%;     // should be 35% - (half of head string length [circa 1% for letter])
    }
</style>