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
            <GridLayout columns="*,*" rows="20,150,*,auto" class="pageBack">
                <!-- image -->
                <StackLayout row="1" col="0">
                    <Image class="imagePlaceholder" />
                </StackLayout>

                <!-- image selector -->
                <FlexboxLayout flexDirection="column" justifyContent="space-around"
                row="1" col="1">
                    <Button text="Z aparatu" />
                    <Button text="Z galerii" />
                </FlexboxLayout>

                <!-- form -->
                <StackLayout row="2" colSpan="2">
                    <StackLayout class="form-group">
                        <TextField v-model="personalData.adress" hint="Adres" class="form-input" />
                    </StackLayout>
                    <StackLayout class="form-group">
                        <TextField v-model="personalData.fatherName" hint="Imię i nazwisko rodzica 1" class="form-input" />
                        <TextField v-model="personalData.motherName" hint="Imię i nazwisko rodzica 2" class="form-input" />
                    </StackLayout>
                    <StackLayout class="form-group">
                        <TextField v-model="personalData.primarySchool" hint="Szkoła podstawowa" class="form-input" />
                    </StackLayout>
                </StackLayout>

                <!-- Save buttons -->
                <Button row="3" col="1" text="Zapisz"/>
            </GridLayout>
        </ScrollView>
    </Page>
</template>

<script lang="ts">
import * as utils from '@/services/sideDrawer/utils';
import { Component, Vue } from "vue-property-decorator";
import { IPersonalData } from '../models/personalDataModel';

@Component
export default class CandidateSettings extends Vue {
    personalData: IPersonalData = {
        adress: '',
        fatherName: '',
        motherName: '',
        primarySchool: ''
    };
    
    onDrawerButtonTap() {
        utils.showDrawer();
    }
}
</script>

<style scoped lang="scss">
    @import '../app-common';

    .imagePlaceholder {
        border-width: 1cm;
        border-color: white;
        width: 140em;
        height: 140em;
    }

    .dummyImage {
        width: 27.5%;     // should be 35% - (half of head string length [circa 1% for letter])
    }
</style>