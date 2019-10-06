<template>
    <Page class="page">
        <ActionBar class="action-bar">
            <Label class="action-bar-title" text="Login"></Label>
        </ActionBar>

        <StackLayout class="loginPageBack">
            <Image class="logoImage"/>

            <StackLayout class="container">
                <FlexboxLayout alignItems="center" class="inputLoginMargin"> 
                    <Label class="info">
                        <FormattedString>
                            <Span class="fa" text.decode="&#xf007; "/>
                        </FormattedString>
                    </Label>
                    <TextField v-model="username" hint="Email" class="form-input" />
                </FlexboxLayout>

                <FlexboxLayout alignItems="center" class="inputPasswordMargin">
                    <Label class="info">
                        <FormattedString>
                            <Span class="fa" text.decode="&#xf023; "/>
                        </FormattedString>
                    </Label>
                    <TextField v-model="password" hint="Password" secure="true" class="form-input" />
                </FlexboxLayout>

                <Button text="Login" @tap="onLoginButtonTap" class="logingButton" />
            </StackLayout>
        </StackLayout>
        
    </Page>
</template>

<script lang="ts">
    import {Component, Vue} from 'vue-property-decorator';

    @Component
    export default class Login extends Vue {
        username: string;
        password: string;
        
        onLoginButtonTap: {
            // not implemented, should call api to authorize 
        }
    };
</script>

<style scoped lang="scss">
    @import '../app-variables';

    .fa {
        color: $warning-light;
    }

    .form-input {
        color: white;
        placeholder-color: white;
        border-bottom-width: 1;
        border-bottom-color: white;
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

    .logingButton {
        background-color: $login-button-color;
        color: white;
        font-weight: bold;
        border-radius: 25;
        padding-top: 14;
        padding-bottom: 14;
        text-transform: uppercase;
        letter-spacing: 0.1;
    }

    .loginPageBack {
        background-image: linear-gradient(to right, $login-left-color, $login-right-color);
    }

    .container {
        margin-left: 34;
        margin-right: 34;
    }
</style>