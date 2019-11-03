<template>
    <Page class="page">
        <ActionBar class="action-bar">
            <Label class="action-bar-title" text="Zarejestruj się"></Label>
        </ActionBar>

        <ScrollView>
            <StackLayout class="pageBack"> <!-- scrollView can have only one child :c -->
                <StackLayout class="form-group">
                    <TextField v-model="userData.name" hint="Imię" class="form-input" />
                    <TextField v-model="userData.surname" hint="Nazwisko" class="form-input" />
                </StackLayout>
    
                <StackLayout class="form-group">
                    <TextField v-model="userData.pesel" class="form-input" hint="Numer PESEL" :editable="!userData.noPesel" />
                    
                    <FlexboxLayout justifyContent="space-between" alignItems="center">
                        <Label>Kliknij, gdy brak numeru PESEL</Label> 
                        <Switch v-model="userData.noPesel" @checkedChange="switchChange()"/>
                    </FlexboxLayout>
                </StackLayout>
    
                <StackLayout class="form-group">
                    <TextField v-model="userData.email" class="form-input" hint="Adres e-mail" />
                    <TextField v-model="userData.password" class="form-input" hint="Hasło" :secure="true" />
                    <TextField v-model="userData.confirmPassword" class="form-input" hint="Powtórz hasło" :secure="true" />
                </StackLayout>
                    
                <FlexboxLayout justifyContent="flex-end">
                    <Button class="my-button" text="Zarejestruj się" @tap="onRegisterTap()"></Button>
                </FlexboxLayout> 
            </StackLayout>
        </ScrollView>

    </Page>
</template>

<script lang="ts">
    import { IRegistrationRequest } from "@/models/userFormModel";
    import { Component, Vue } from 'vue-property-decorator';
    import { UserService } from '@/services/userService/userService';
    import { AxiosResponse } from 'axios';
    import ConnectionService from "@/services/common/connection";
    import PopupFactory from "@/services/popupFactory";

    @Component
    export default class Register extends Vue {
        userData: IRegistrationRequest = {
           email: "",
           password: "",
           confirmPassword: "",
           name: "",
           surname: "",
           pesel: "",
           noPesel: false,
        }
        userService: UserService = new UserService();

        onRegisterTap() {
            if (ConnectionService.IsConnectedToNetwork()) {
                this.userService.register(this.userData).then((data) => {
                    console.log("register ok");
                }).catch(error => console.log(error));
            }
            else {
                PopupFactory.ConnectionError();
            }
        }

        switchChange() {
            this.userData.pesel = "";
        }
    };
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