<template>
    <Page actionBarHidden="true" class="page">
        <StackLayout class="loginPageBack">
            <GridLayout rows="*, 200" class="container">
                <Image class="logoImage" row="0"/>
                
                <FlexBoxLayout row="1" justifyContent="space-between" alignItems="center">
                    <Button class="bottomButton" row="1" 
                        @tap="$goto.Login()" text="zaloguj" />
                    <Button class="bottomButton" row="1"
                        @tap="$goto.Register()" text="zarejestruj" />
                </FlexBoxLayout>
            </GridLayout>
        </StackLayout>
    </Page>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
export default class Home extends Vue {
    
}
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
        width: 140em;
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
