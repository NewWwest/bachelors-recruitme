import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../../services/user.service';
import VueRouter from 'vue-router';
import { RecruitmentService } from '../../../services/recruitment.service';

// @ts-ignore
@Component
export default class PersonalData extends Vue {
    name: string = "";
    surname: string = "";

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
            console.log(resp);
        }, (err) => {
            console.error(err);
        })
    }

    handleSubmit() {
        let request = {
            name: this.name,
            surname: this.surname
        }

        this.recruitmentService.updatePersonalData(request)
    }
}
