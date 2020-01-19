<template>
    <Page class="page">
        <ActionBar class="action-bar">
            <Label class="action-bar-title" text="Login"></Label>
        </ActionBar>
        <StackLayout class="loginPageBack">
            <Image class="logoImage"/>

            <StackLayout class="container">
                <StackLayout class="inputLoginMargin">
                    <FlexboxLayout alignItems="center"> 
                        <Label class="info">
                            <FormattedString>
                                <Span class="fa" text.decode="&#xf007; "/>
                            </FormattedString>
                        </Label>
                        <TextField v-model="username" hint="Login" autocorrect="false" autocapitalizationType="none"
                        :class="[!validUserName() ? 'form-input' : 'error-input', 'input-width']"/>
                    </FlexboxLayout>
                    <Label v-show="validUserName()" class="error-label"
                    text="Login jest wymagany" /> 
                </StackLayout>

                <StackLayout class="inputPasswordMargin">
                    <FlexboxLayout alignItems="center">
                        <Label class="info">
                            <FormattedString>
                                <Span class="fa" text.decode="&#xf023; "/>
                            </FormattedString>
                        </Label>
                        <TextField v-model="password" hint="Password" secure="true" 
                        v-bind:class="[!validPassword() ? 'form-input' : 'error-input', 'input-width']" />
                    </FlexboxLayout>
                    <Label v-show="validPassword()" class="error-label"
                    text="Hasło jest wymagane" />
                </StackLayout>

                <Button text="Login" @tap="onLoginButtonTap" class="my-button" />

                <FlexBoxLayout class="form-group" justifyContent="space-between" alignItems="center">
                    <Label @tap="$goto.RemindLogin()" text="Przypomnij login" />
                    <Label @tap="$goto.ResetPassword()" text="Zresetuj hasło" />
                </FlexBoxLayout>
            </StackLayout>
        </StackLayout>       
    </Page>
</template>

<script lang="ts">
import { UserService } from '../services/userService/userService';
import { Component, Vue } from 'vue-property-decorator';
import ConnectionService from '../services/common/connection';
import PopupFactory from '@/services/popupFactory';
import LoaderService from '@/services/loaderView/loader';

    @Component
    export default class Login extends Vue {
        username: string = "";
        password: string = "";
        submitted: boolean = false;
        userService: UserService = new UserService();

        onLoginButtonTap() {
            this.submitted = true;
            if (!this.username || !this.password) 
                    return;

            if (ConnectionService.IsConnectedToNetwork()) {
                LoaderService.showLoader();

                this.userService.login(this.username, this.password).then(() => {
                    LoaderService.hideLoader();
                    this.$goto.CandidateDashboard(true);
                }, err => {
                    console.error(err);
                    
                    LoaderService.hideLoader();
                    PopupFactory.GenericErrorPopup("" + err);
                })
            }
            else {
                PopupFactory.ConnectionError();
            }
        }

        validPassword() {
            return this.submitted && !this.password;
        }
        validUserName() {
            return this.submitted && !this.username;
        }
    };
</script>

<style scoped lang="scss">
    @import '../app-common';
    @import '../app-variables';

    .fa {
        color: #cf995f;
    }

    .input-width {
        width: 90%;
    }

    .inputLoginMargin {
        padding-bottom: 8;
    }
    .inputPasswordMargin {
        margin-bottom: 30;
        padding-bottom: 8;
        letter-spacing: 0.1;
    }

    .logoImage {
        border-color: white;
        border-width: 1cm;
        width: 150em;
        height: 150em;
        margin-bottom: 30;
        margin-top: 50;
    }

    .loginPageBack {
        background-image: linear-gradient(to right, $login-left-color, $login-right-color);
    }

    .container {
        margin-left: 34;
        margin-right: 34;
    }
</style>