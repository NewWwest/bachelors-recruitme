<template>
  <Page class="page">
    <ActionBar class="action-bar">
        <Label class="action-bar-title" text="Przypomnij login"></Label>
    </ActionBar>

    <ScrollView>
        <StackLayout class="pageBack"> <!-- scrollView can have only one child :c -->
            <StackLayout class="form-group">
                <TextField v-model="remindData.email" class="form-input" hint="Adres e-mail" />
            </StackLayout>

            <StackLayout class="form-group">
                <TextField v-model="remindData.pesel" class="form-input" hint="Numer PESEL"
                 keyboardType="number" :editable="!noPesel" />
                
                <FlexboxLayout justifyContent="space-between" alignItems="center">
                    <Label>Kliknij, gdy brak numeru PESEL</Label> 
                    <Switch v-model="noPesel" @checkedChange="switchChange()"/>
                </FlexboxLayout>
            </StackLayout>

            <StackLayout v-if="noPesel" class="form-group">
                <TextField v-model="remindData.name" hint="Imię" class="form-input" />
                <TextField v-model="remindData.surname" hint="Nazwisko" class="form-input" />
            </StackLayout>
                
            <FlexboxLayout class="form-group" justifyContent="flex-end">
                <Button class="my-button" text="Przypomnij login" @tap="onRemindTap()"></Button>
            </FlexboxLayout> 
        </StackLayout>
    </ScrollView>
  </Page>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { UserService } from '../services/userService/userService';
import { IRemindLoginRequest } from '../models/userFormModel';
import ConnectionService from '../services/common/connection';
import PopupFactory from '../services/popupFactory';

@Component
export default class RemindLogin extends Vue {
    loginReminded: boolean = false;
    noPesel: boolean = false;
    remindData: IRemindLoginRequest = {
        email: '',
        name: '',
        surname: '',
        pesel: ''
    };
    userService: UserService = new UserService();

    onRemindTap() {
        if (ConnectionService.IsConnectedToNetwork()) {
            this.userService.remindLogin(this.remindData).then(() => {
                this.loginReminded = true;
            })
        }
        else {
            PopupFactory.ConnectionError();
        }
    }

    switchChange() {
        this.remindData.pesel = "";
    }
}
</script>

<style scoped lang="scss">
    @import '../app-common';
    @import '../app-variables';

    .pageBack {
        background-image: linear-gradient(to right, $login-left-color, $login-right-color);
        padding-left: 30;
        padding-right: 30;
    }

    .form-input {
        color: white;
        placeholder-color: white;
        border-bottom-width: 1;
        border-bottom-color: white;
        margin-bottom: 50px;
    }

    .form-group {
        margin-top: 15;
    }

    Label {
        color: #E3E3E3;
        font-size: 16px;
    }

    Switch[checked=true] {
        color: #F88533;
        background-color: #F17C7C;
    }

    TextField[editable=false] {
        color: #555555;
        placeholder-color: #555555;
        border-bottom-color: #555555;
    }
</style>