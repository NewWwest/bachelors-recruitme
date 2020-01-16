<template>
    <Page @loaded="onNavigatingTo" class="page">
        <ActionBar class="action-bar">
            <StackLayout horizontalAlignment="left" orientation="horizontal">
                <Image src="res://mobile_menu_white" width="32" height="32"
                    @tap="onDrawerButtonTap"/>
                <Image class="dummyImage" /> <!-- dummy object to get text to center -->
                <Label class="action-bar-title" text="Egzaminy"></Label>
            </StackLayout>  
        </ActionBar>

        <GridLayout rows="auto,*,auto">
            <NotFilledPersonalData row="0"></NotFilledPersonalData>
            <StackLayout row="1" class="pageBack">
                <ScrollView height="95%">
                    <ListView for="exam in exams" separatorColor="transparent" class="list">
                        <v-template>
                            <GridLayout columns="5,5*,4*,5" rows="auto,auto" class="exam-card">
                                <Label row="0" col="1" :text="exam.categoryName" class="text-bf"/>
                                <Label row="1" col="1" :text="getExamTypeText(exam.examType)" />

                                <Label row="0" col="2" :text="getDateText(exam.startTime)" horizontalAlignment="right" />
                                <Label row="1" col="2" :text="getMinutesText(exam.durationInMinutes)" horizontalAlignment="right"/>
                            </GridLayout>
                        </v-template>
                    </ListView>
                </ScrollView>
            </StackLayout>
        </GridLayout>
    </Page>
</template>

<script lang="ts">
import NotFilledPersonalData from '@/components/common/NotFilledPersonalData.vue'
import * as utils from '@/services/sideDrawer/utils';
import { Component, Vue } from "vue-property-decorator";
import { PersonalDataService } from '../services/personalData/personalDataService';
import { toLocaleDateTimeString } from '../services/dateTime/dateTimeHelper';

@Component({
    components: { NotFilledPersonalData }
})
export default class Exams extends Vue {
    exams: [] = [];
    personalDataService: PersonalDataService = new PersonalDataService();

    getExamTypeText(examType: number): string {
        return examType == 1 ? "Indywidualny" : "Pisemny";
    }

    getDateText(startTime: string): string {
        const date: Date = new Date(startTime);

        let str = toLocaleDateTimeString(date);
        const idx = str.lastIndexOf(':');

        return str.substring(0, idx);
    }

    getMinutesText(durationInMinutes: number): string {
        let suffix: string;
        if (durationInMinutes < 4) {
            if (durationInMinutes == 1) suffix = " minuta";
            else suffix = " minuty";
        }
        else suffix = " minut";

        return durationInMinutes + suffix;
    }

    onNavigatingTo() {
        this.personalDataService.examsAndStatus().then(exams => {
            this.exams = exams;
        })
    }

    onDrawerButtonTap() {
        utils.showDrawer();
    }
}
</script>

<style scoped lang="scss">
    @import '../app-common';
    .text-bf {
        font-weight: bold;
    }
    .list {
        margin-top: 20;
    }
    .exam-card {
        border-radius: 10;
        background-color: #cccccc;
        padding: 5;
    }
    .dummyImage {
        width: 28%;     // should be 35% - (half of head string length [circa 1% for letter])
    }
</style>