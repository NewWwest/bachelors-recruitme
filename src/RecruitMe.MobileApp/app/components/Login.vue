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

                <Button text="Login" @tap="onLogginButtonTap" class="loggingButton" />
            </StackLayout>
        </StackLayout>
        
    </Page>
</template>

<script>
    export default {
        data() {
            return {
                username: '',
                password: ''
            }
        }
    };
</script>

<style scoped lang="scss">
    // Start custom common variables
    @import '../app-variables';
    // End custom common variables

    // Custom styles
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

    .loginPageBack {
        background-image: linear-gradient(to right, #39B994, #337FC7);
    }

    .logoImage {
        border-color: white;
        border-width: 1cm;
        width: 150em;
        height: 150em;
        margin-bottom: 30;
        margin-top: 50;
    }

    .loggingButton {
        background-color: #4BD5DC;
        color: white;
        font-weight: bold;
        border-radius: 25;
        padding-top: 14;
        padding-bottom: 14;
        text-transform: uppercase;
        letter-spacing: 0.1;
    }

    .container {
        margin-left: 34;
        margin-right: 34;
    }
</style>
