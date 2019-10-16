import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../../services/user.service';
import VueRouter from 'vue-router';
import { RecruitmentService } from '../../../services/recruitment.service';

// @ts-ignore
@Component
export default class PersonalData extends Vue {
    adress: string = "";
    fatherName: string = "";
    motherName: string = "";
    primarySchool: string = "";

    submitted: boolean = false;
    fetching: boolean = false;

    userService: UserService = new UserService();
    recruitmentService: RecruitmentService = new RecruitmentService();

    constructor() {
        super();
    }

    mounted() {
        if (!this.userService.isLoggedIn()) {
            var router = new VueRouter();
            router.go(-1);
        }
        this.recruitmentService.getPersonalData().then((resp) => {
            this.adress = resp.adress;
            this.fatherName = resp.fatherName;
            this.motherName = resp.motherName;
            this.primarySchool = resp.primarySchool;
        }, (err) => {
            console.error(err);
        })
    }

    handleSubmit() {
        let request = {
            adress: this.adress,
            fatherName: this.fatherName,
            motherName: this.motherName,
            primarySchool: this.primarySchool
        }

        this.recruitmentService.updatePersonalData(request)
    }
}
