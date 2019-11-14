<template>
  <GridLayout rows="auto, *" class="nt-drawer__content">
    <StackLayout row="0" class="nt-drawer__header">
        <Image class="nt-drawer__header-image fas t-36" src.decode="font://&#xf2bd;"></Image>
        <Label class="nt-drawer__header-brand" text="User Name"></Label>
        <Label class="nt-drawer__header-footnote" text="username@mail.com"></Label>
    </StackLayout>

    <ScrollView row="1" class="nt-drawer__body">
        <StackLayout>
            <GridLayout columns="auto, *" 
             :class="'nt-drawer__list-item' + (selectedPage === 'Home' ? ' -selected': '')" 
             @tap="onNavigationItemTap($goto.Home())">
                <Label col="0" text.decode="&#xf015;" class="nt-icon fas"></Label>
                <Label col="1" text="Home" class="p-r-10"></Label>
            </GridLayout>

            <!-- <GridLayout columns="auto, *" 
             :class="'nt-drawer__list-item' + (selectedPage === 'Browse' ? ' -selected': '')"
             @tap="onNavigationItemTap(Browse)">
                <Label col="0" text.decode="&#xf1ea;" class="nt-icon far"></Label>
                <Label col="1" text="Browse" class="p-r-10"></Label>
            </GridLayout>

            <GridLayout columns="auto, *" 
             :class="'nt-drawer__list-item' + (selectedPage === 'Search' ? ' -selected': '')" 
             @tap="onNavigationItemTap(Search)">
                <Label col="0" text.decode="&#xf002;" class="nt-icon fas"></Label>
                <Label col="1" text="Search" class="p-r-10"></Label>
            </GridLayout>

            <GridLayout columns="auto, *"
             :class="'nt-drawer__list-item' + (selectedPage === 'Featured' ? ' -selected': '')"
             @tap="onNavigationItemTap(Featured)">
                <Label col="0" text.decode="&#xf005;" class="nt-icon fas"></Label>
                <Label col="1" text="Featured" class="p-r-10"></Label>
            </GridLayout> -->

            <StackLayout class="hr"></StackLayout>

            <GridLayout columns="auto, *" 
             :class="'nt-drawer__list-item' + (selectedPage === 'Settings' ? ' -selected': '')"
             @tap="onNavigationItemTap($goto.Settings())">
                <Label col="0" text.decode="&#xf013;" class="nt-icon fas"></Label>
                <Label col="1" text="Settings" class="p-r-10"></Label>
            </GridLayout>
        </StackLayout>
    </ScrollView>
  </GridLayout>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

import { closeDrawer } from "@/services/sideDrawer/utils";
import SelectedPageService from "@/services/sideDrawer/selectedPage.service"

@Component
export default class DrawerContent extends Vue {
    selectedPage: string = "";
    
    mounted () {
        SelectedPageService.getInstance().selectedPage$
                .subscribe((selectedPage) => this.selectedPage = selectedPage);
    }

    onNavigationItemTap(navigateAction: () => void) : void {
        navigateAction();
        closeDrawer();
    }
}
</script>

<style>

</style>