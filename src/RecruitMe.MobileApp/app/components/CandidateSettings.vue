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
            <GridLayout columns="*,*" rows="20,auto,*,auto" class="pageBack">
                <!-- image -->
                <StackLayout row="1" col="0">
                    <Image class="imagePlaceholder" :src="ImageSrc"/>
                </StackLayout>

                <!-- image selector -->
                <FlexboxLayout flexDirection="column" justifyContent="space-around"
                row="1" col="1">
                    <Button class="my-button" text="Z aparatu" @tap="onTakeImageTap"/>
                    <Button class="my-button" text="Z galerii" />
                </FlexboxLayout>

                <!-- form -->
                <StackLayout row="2" colSpan="2">
                    <StackLayout class="form-group">
                        <TextField v-model="profileData.adress" hint="Adres" class="form-input" />
                    </StackLayout>
                    <StackLayout class="form-group">
                        <TextField v-model="profileData.fatherName" hint="Imię i nazwisko rodzica 1" class="form-input" />
                        <TextField v-model="profileData.motherName" hint="Imię i nazwisko rodzica 2" class="form-input" />
                    </StackLayout>
                    <StackLayout class="form-group">
                        <TextField v-model="profileData.primarySchool" hint="Szkoła podstawowa" class="form-input" />
                    </StackLayout>
                </StackLayout>

                <!-- Save buttons -->
                <Button row="3" colSpan="2" class="saveBtn my-button" text="Zapisz"
                 @tap="onSaveButtonTap"/>
            </GridLayout>
        </ScrollView>
    </Page>
</template>

<script lang="ts">
import * as utils from '@/services/sideDrawer/utils';
import { Component, Vue } from "vue-property-decorator";
import { IProfileData } from '../models/personalDataModel';
import { PersonalDataService } from '../services/personalData/personalDataService';
import ConnectionService from '../services/common/connection';
import LoaderService from '../services/loaderView/loader';
import PopupFactory from '../services/popupFactory';
import { LocalStorageService } from '../services/localStorage/localStorageService';
import { ImageService } from '../services/image/imageService';
import { ImageSource } from 'tns-core-modules/image-source/image-source';

@Component
export default class CandidateSettings extends Vue {
    imageSrc: ImageSource = new ImageSource();
    imageService: ImageService = new ImageService();
    profileData: IProfileData = {
        adress: '',
        fatherName: '',
        motherName: '',
        primarySchool: '',
        profilePictureName: '',
        profilePictureFileId: 0
    };
    personalDataService: PersonalDataService = new PersonalDataService();
    
    constructor() {
        super();
        
        const data = LocalStorageService.getProfileData();

        if (data !== null) {
            this.profileData = data;
        }
    }

    beforeMount() {
        this.reloadPicture();
    }

    reloadPicture() {
        this.imageService.loadUserPicture().then(r => this.imageSrc = r)
         .then(r => LoaderService.hideLoader());
    }

    onDrawerButtonTap() {
        utils.showDrawer();
    }

    onTakeImageTap() {
        this.imageService.takePicture().then(r => {
            this.personalDataService.getProfileData().then(r1 => {
                if (r1) {
                    this.reloadPicture();
                }
            })
        })
    }

    onSaveButtonTap() {
        if (ConnectionService.IsConnectedToNetwork()) {
            LoaderService.showLoader();
            this.personalDataService.setProfileData(this.profileData).then(
                r => LoaderService.hideLoader(), e => LoaderService.hideLoader()
            );
        }
        else {
            PopupFactory.ConnectionError();
        }
    }

    get ImageSrc() {
        return this.imageSrc;
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

    .saveBtn {
        height: 50em;
        margin-top: 50px;
    }

    .dummyImage {
        width: 27.5%;     // should be 35% - (half of head string length [circa 1% for letter])
    }
</style>