<template>
    <Page class="page">
        <ActionBar class="action-bar">
            <FlexBoxLayout>
                <NavigationButton class="fa" text.decode="&#xf0c9; " @tap="onDrawerButtonTap">
                </NavigationButton>
                <Label class="action-bar-title" text="Home"></Label>
            </FlexBoxLayout>
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
</style>