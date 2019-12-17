<template>
  <GridLayout v-if="visible" columns="*,auto,*,auto" class="backGr">
      <Label col="1" @tap="onTextClick" class="warning-text"
       text="Uzupełnij swoje dane osobiste!" />
      <Label col="3" @tap="onTimesClick" class="cancel-icon">
        <FormattedString>
            <Span class="fa" text.decode="&#xf00d; "/>
        </FormattedString>
      </Label>
  </GridLayout>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { LocalStorageService } from '../../services/localStorage/localStorageService';

@Component
export default class NotFilledPersonalData extends Vue {
    visible: boolean;
    
    constructor() {
        super();
        let profileData = LocalStorageService.getProfileData();

        if (profileData == null) {
            throw new Error("Personal Data not checked!");
        }

        this.visible = profileData ? profileData.adress && profileData.fatherName &&
            profileData.motherName && profileData.primarySchool ? false : true : true;
    }

    onTimesClick() {
        this.visible = false;
    }
    onTextClick() {
        this.$goto.CandidateSettings();
    }
}
</script>

<style scoped lang="scss">
    @import '../../app-common';

    .backGr {
        background-color: #8B0000;
        color: #E3E3E3;
        height: 70px;
    }
    .warning-text {
        text-decoration: underline;
        margin-top: 5px;
    }
    .cancel-icon {
        margin-top: 10px;
    }
</style>