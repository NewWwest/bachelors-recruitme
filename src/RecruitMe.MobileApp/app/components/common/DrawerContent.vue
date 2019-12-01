<template>
  <GridLayout rows="auto, *" class="drawer__content">
    <FlexboxLayout row="0" class="drawer__header"
     flexDirection="row" justifyContent="space-around" alignItems="center">
        <Image class="drawer__header-image fa t-75" src.decode="font://&#xf2bd;"></Image>
        <StackLayout>
            <Label class="drawer__header-name" :text="username"></Label>
            <Label class="drawer__header-email" :text="email"></Label>
        </StackLayout>
    </FlexboxLayout>

    <ScrollView row="1" class="drawer__list">
        <StackLayout>
            <FlexboxLayout flexDirection="row" alignItems="center"
             :class="'drawer__list-item' + (selectedPage === 'CandidateDashboard' ? '-selected': '')" 
             @tap="onNavigationItemTap(() => $goto.CandidateDashboard(true))">
                <Label text.decode="&#xf015;" class="fa item-icon"></Label>
                <Label text="Home"></Label>
            </FlexboxLayout>

            <FlexboxLayout flexDirection="row" alignItems="center"
             :class="'drawer__list-item' + (selectedPage === 'CandidateSettings' ? '-selected': '')"
             @tap="onNavigationItemTap(() => $goto.CandidateSettings(true))">
                <Label text.decode="&#xf013;" class="fa item-icon"></Label>
                <Label text="Settings"></Label>
            </FlexboxLayout>
        </StackLayout>
    </ScrollView>
  </GridLayout>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

import { closeDrawer } from "@/services/sideDrawer/utils";
import SelectedPageService from "@/services/sideDrawer/selectedPage.service"
import { LocalStorageService } from '../../services/userService/localStorageService';

@Component
export default class DrawerContent extends Vue {
    selectedPage: string = "Home";
    
    mounted () {
        SelectedPageService.getInstance().selectedPage$
                .subscribe((selectedPage) => {
                    this.selectedPage = selectedPage;
                    console.log(this.selectedPage);
                });
    }

    onNavigationItemTap(navigateAction: () => void) : void {
        navigateAction();
        closeDrawer();
    }

    get email() {
        return LocalStorageService.getEmail();
    }
    get username() {
        return LocalStorageService.getFullname();
    }
}
</script>

<style scoped lang="scss">
@import '../../app-variables';

    .fa {
        font-family: "FontAwesome";
    }

    .item-icon {
        margin: 0 20;
    }

    .t-75 {
        width: 75;
    }

    .drawer {
        &__header {
            padding: 30 0;
            background-color: $login-button-color;

            &-image {
                background-color: #cccccc;
                border-radius: 25;
            }
        }

        &__list {
            background-color: $login-left-color;

            &-item {
                padding: 5 0;

                Label {
                    font-size: 24;
                }

                &-selected {
                    padding: 5 0;

                    Label {
                        font-size: 24;
                    }
                    background-color: darken($color: $login-left-color, $amount: 10);
                }
            }
        }
    }
</style>