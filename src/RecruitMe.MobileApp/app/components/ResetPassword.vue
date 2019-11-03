<template>
  <Page class="page">
    <ActionBar class="action-bar">
        <Label class="action-bar-title" text="Zresetuj hasło"></Label>
    </ActionBar>

    <ScrollView>
        <StackLayout class="pageBack"> <!-- scrollView can have only one child :c -->
            <StackLayout class="form-group">
                <TextField v-model="resetData.login" class="form-input" hint="Login" />
            </StackLayout>
                
            <FlexboxLayout class="form-group" justifyContent="flex-end">
                <Button class="my-button" text="Zresetuj hasło" @tap="onResetTap()"></Button>
            </FlexboxLayout> 
        </StackLayout>
    </ScrollView>
  </Page>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { UserService } from '../services/userService/userService';
import { IResetPasswordRequest } from '../models/userFormModel';
import PopupFactory from '../services/popupFactory';
import ConnectionService from '@/services/common/connection';

@Component
export default class ResetPassword extends Vue {
    resetData: IResetPasswordRequest = {
        login: ''
    };
    userService: UserService = new UserService();


    onResetTap() {
        if (ConnectionService.IsConnectedToNetwork()) {
            this.userService.resetPassword(this.resetData);
        }
        else {
            PopupFactory.ConnectionError();
        }
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