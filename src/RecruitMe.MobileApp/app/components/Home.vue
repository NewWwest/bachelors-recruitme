<template>
    <Page class="page">
        <ActionBar class="action-bar">
            <Label class="action-bar-title" text="Home"></Label>
        </ActionBar>

        <StackLayout class="loginPageBack">
            <GridLayout rows="*, 200" class="container">
                <Image class="logoImage" row="0"/>
                
                <FlexBoxLayout row="1" justifyContent="space-between" alignItems="center">
                    <Button class="bottomButton" row="1" 
                        @tap="$goto('login')" text="login" />
                    <Button class="bottomButton" row="1"
                        @tap="onRegisterButtonTap" text="register" />
                </FlexBoxLayout>
            </GridLayout>
        </StackLayout>
    </Page>
</template>

<script>
    export default {
        data() {

        },
        methods: {
            onRegisterButtonTap: function(e) {

            }
        }
    };
</script>

<style scoped lang="scss">
    @import '../app-variables';

    .logoImage {
        border-color: white;
        border-width: 1cm;
        width: 300em;
        height: 300em;
        margin-bottom: 0;
        margin-top: 50;
    }

    .bottomButton {
        background-color: $login-button-color;
        color: white;
        font-weight: bold;
        border-radius: 25;
        padding-top: 14;
        padding-bottom: 14;
        text-transform: uppercase;
        letter-spacing: 0.1;
        width: 120em;
        height: 55em;
    }

    .loginPageBack {
        background-image: linear-gradient(to right, $login-left-color, $login-right-color);
    }

    .container {
        margin-left: 34;
        margin-right: 34;
    }
</style>
