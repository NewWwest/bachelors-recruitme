<template>
    <Page class="page">
        <ActionBar class="action-bar">
            <Label class="action-bar-title" text="Register"></Label>
        </ActionBar>

        <StackLayout class="pageBack">
            <StackLayout>
                <Label class="form-label">Imię</Label>
                <TextField v-model="userForm.firstName" class="form-input" />
            </StackLayout>
            <StackLayout>
                <Label class="form-label">Nazwisko</Label>
                <TextField v-model="userForm.lastName" class="form-input" />
            </StackLayout>

            <StackLayout>
                <Label class="form-label">PESEL</Label>
                <TextField v-model="userForm.pesel" class="form-input" :editable="userForm.ifPesel" />
                
                <FlexboxLayout justifyContent="space-between" alignItems="center">
                    <Label>Zaznacz, jeżeli brak numeru PESEL</Label> 
                    <Switch v-model="userForm.ifPesel" (checkedChange)="switchChange()"/>
                </FlexboxLayout>
            </StackLayout>

            <StackLayout>
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
            </StackLayout>

            <StackLayout>
                <Label class="form-label">Hasło</Label>
                <TextField v-model="userForm.password" class="form-input" :secure="true" />
            </StackLayout>

        </StackLayout>

    </Page>
</template>

<script lang="ts">
    import UserForm from "../models/UserForm";
    import {Component, Vue} from 'vue-property-decorator';

    @Component
    export default class Register extends Vue {
        userForm: UserForm = new UserForm();

        // onLoginButtonTap(): {
        //     // not implemented, should call api to authorize 
        // }
        // get peselDisabled() {
        //     return this.userForm.ifPesel;
        // }

        switchChange() {
            this.userForm.pesel = "";
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