import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../../services/user.service';
import VueRouter from 'vue-router';
import { RecruitmentService } from '../../../services/recruitment.service';
import { PictureConfirmedEvent } from './pictureInput/pictureConfirmed.event';
import { IProfileData } from '../../../models/recruit.models';
import { ApiGateway } from '../../../api/api.gateway';

@Component({components: {
    "PictureInput": require('./pictureInput/pictureInput.vue.html').default
  }
})
export default class ProfileComponent extends Vue {
    adress: string = "";
    fatherName: string = "";
    motherName: string = "";
    primarySchool: string = "";
    profilePicName: string = "";
    profilePictureFileId: number = -1;

    profile: IProfileData = {} as IProfileData;
    birthDate: string = "";
    submitted: boolean = false;
    fetching: boolean = false;

    file: any = null;

    userService: UserService = new UserService();
    recruitmentService: RecruitmentService = new RecruitmentService();
    apiGateway: ApiGateway = new ApiGateway();

    constructor() {
        super();
    }

    mounted() {
        if (!this.userService.isLoggedIn()) {
            var router = new VueRouter();
            router.go(-1);
            alert("unathorized");
        }
        this.recruitmentService.getProfile().then(this.updateLocals);
    }

    PictureConfirmed(a: PictureConfirmedEvent): void {
        this.recruitmentService.setNewProfilePicture(a.pictureName, a.pictureFile).then(this.updateLocals);
    }

    handleSubmit() {
        if (this.fetching)
            return;

        let request = {
            adress: this.adress,
            fatherName: this.fatherName,
            motherName: this.motherName,
            primarySchool: this.primarySchool,
        }

        this.recruitmentService.updatePersonalData(request).then(this.updateLocals);
    }

    uploadDocument() {
        if (this.fetching)
            return;

        this.fetching = true;
        this.recruitmentService.uploadDocument(this.file.name, this.file).then(this.updateLocals);
    }

    downloadDocument(fileId: number) {
        let doc = this.profile.documents.find(d => d.id == fileId);
        if (doc) {
            this.apiGateway.downloadDocument(fileId, doc.name);
        }
    }

    deleteDocument(fileId: number) {
        if (this.fetching)
            return;

        this.fetching = true;
        this.recruitmentService.deleteDocument(fileId).then(this.updateLocals);
    }

    updateLocals(resp: IProfileData) {
        this.profile = resp;
        this.birthDate = new Date(resp.birthDate).toLocaleDateString("pl");
        this.adress = resp.adress;
        this.fatherName = resp.fatherName;
        this.motherName = resp.motherName;
        this.primarySchool = resp.primarySchool;
        this.profilePicName = resp.profilePictureName;
        this.profilePictureFileId = resp.profilePictureFileId ? resp.profilePictureFileId : -1;
        this.fetching = false;
    }
}
