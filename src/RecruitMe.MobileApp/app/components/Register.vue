<template>
    <Page class="page">
        <ActionBar class="action-bar">
            <Label class="action-bar-title" text="Register"></Label>
        </ActionBar>

        <!-- <StackLayout class="pageBack"> -->
            <ScrollView>
                <StackLayout class="pageBack"> <!-- scrollView can have only one child :c -->
                    <StackLayout>
                        <Label class="form-label">Imię</Label>
                        <TextField v-model="userData.name" class="form-input" />
                    </StackLayout>
                    <StackLayout>
                        <Label class="form-label">Nazwisko</Label>
                        <TextField v-model="userData.surname" class="form-input" />
                    </StackLayout>
        
                    <StackLayout>
                        <Label class="form-label">PESEL</Label>
                        <TextField v-model="userData.pesel" class="form-input" :editable="!userData.noPesel" />
                        
                        <FlexboxLayout justifyContent="space-between" alignItems="center">
                            <Label>Zaznacz, jeżeli brak numeru PESEL</Label> 
                            <Switch v-model="userData.noPesel" @checkedChange="switchChange()"/>
                        </FlexboxLayout>
                    </StackLayout>
        
                    <!-- <StackLayout>
                        <Label class="form-label">Adres</Label>
                        <TextField v-model="userForm.address" class="form-input" />
                    </StackLayout>
        
                    <StackLayout>
                        <Label class="form-label">Imię matki</Label>
                        <TextField v-model="userForm.motherName" class="form-input" />
                    </StackLayout>
        
                    <StackLayout>
                        <Label class="form-label">Imię ojca</Label>
                        <TextField v-model="userForm.fatherName" class="form-input" />
                    </StackLayout>
        
                    <StackLayout>
                        <Label class="form-label">Nazwa szkoły</Label>
                        <TextField v-model="userForm.schoolName" class="form-input" />
                    </StackLayout> -->
        
                    <StackLayout>
                        <Label class="form-label">Hasło</Label>
                        <TextField v-model="userData.password" class="form-input" :secure="true" />
                    </StackLayout>

                    <StackLayout>
                        <Label class="form-label">Powtórz hasło</Label>
                        <TextField v-model="userData.confirmPassword" class="form-input" :secure="true" />
                    </StackLayout>

                    <FlexboxLayout justifyContent="flex-end">
                        <Button text="Zarejestruj się" @tap="onRegisterTap()"></Button>
                    </FlexboxLayout> 
                </StackLayout>
            </ScrollView>
        <!-- </StackLayout> -->

    </Page>
</template>

<script lang="ts">
    import { IRegistrationRequest } from "../models/userFormModel";
    import { Component, Vue } from 'vue-property-decorator';
    import { UserService } from '../services/userService/userService';
    import { AxiosResponse } from 'axios';

    @Component
    export default class Register extends Vue {
        email = "";
        password = "";
        confirmPassword = "";
        name = "";
        surname = "";
        pesel = "";
        noPesel = false; 
        
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
            this.userService.register(this.userData).then((data) => {
                console.log("ok");
            }).catch(error => console.log(error));
        }

        switchChange() {
            this.userData.pesel = "";
        }
    };
</script>

<style scoped lang="scss">
    @import '../app-variables';

    .form-input {
        color: white;
        placeholder-color: white;
        border-bottom-width: 1;
        border-bottom-color: white;
        margin-bottom: 50px;
    }

    .pageBack {
        background-image: linear-gradient(to right, $login-left-color, $login-right-color);
        padding: 30;
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