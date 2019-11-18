import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../../services/user.service';
import VueRouter from 'vue-router';
import { RecruitmentService } from '../../../services/recruitment.service';
import { PictureConfirmedEvent } from '../../shared/pictureInput/PictureConfirmedEvent';

@Component({components: {
    "PictureInput": require('../../shared/pictureInput/pictureInput.vue.html').default
  }
})
export default class ProfileComponent extends Vue {
    adress: string = "";
    fatherName: string = "";
    motherName: string = "";
    primarySchool: string = "";
    profilePicName: string = "";
    profilePictureFileId: number | undefined;

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
            alert("unathorized");
        }
        this.recruitmentService.getPersonalData().then((resp) => {
            this.adress = resp.adress;
            this.fatherName = resp.fatherName;
            this.motherName = resp.motherName;
            this.primarySchool = resp.primarySchool;
            this.profilePicName = resp.profilePictureName;
            this.profilePictureFileId = resp.profilePictureFileId;
        }, (err) => {
            console.error(err);
        })
    }

    PictureConfirmed(a: PictureConfirmedEvent): void {
        this.recruitmentService.setNewProfilePicture(a.pictureName, a.pictureFile);
    }

    handleSubmit() {
        let request = {
            adress: this.adress,
            fatherName: this.fatherName,
            motherName: this.motherName,
            primarySchool: this.primarySchool,
        }

        this.recruitmentService.updatePersonalData(request)
    }
}
