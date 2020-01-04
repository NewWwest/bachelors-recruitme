import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../../services/user.service';
import VueRouter from 'vue-router';
import { RecruitmentService } from '../../../services/recruitment.service';
import { PictureConfirmedEvent } from './pictureInput/pictureConfirmed.event';
import { IProfileData } from '../../../models/recruit.models';
import { ApiGateway } from '../../../api/api.gateway';
import { getErrorMessage } from '../../../helpers/error.helper';
import { MessageBusService } from '../../../services/messageBus.service';

@Component({
    components: {
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
    fetching: boolean = false;

    file: any = null;

    userService: UserService = new UserService();
    recruitmentService: RecruitmentService = new RecruitmentService();
    apiGateway: ApiGateway = new ApiGateway();

    mounted() {
        if (!this.userService.isLoggedIn()) {
            var router = new VueRouter();
            router.go(-1);
        }
        this.recruitmentService.getProfile().then(this.updateLocals, this.handleError);
    }

    PictureConfirmed(a: PictureConfirmedEvent): void {
        if (this.fetching)
            return;
        this.fetching = true;

        this.recruitmentService.setNewProfilePicture(a.pictureName, a.pictureFile).then(this.updateLocals, this.handleError);
    }

    handleSubmit() {
        if (this.fetching)
            return;
        this.fetching = true;

        let request = {
            adress: this.adress,
            fatherName: this.fatherName,
            motherName: this.motherName,
            primarySchool: this.primarySchool,
        }

        this.recruitmentService.updatePersonalData(request).then(this.updateLocals, this.handleError);
    }

    uploadDocument() {
        if (this.fetching)
            return;
        if (!this.file) {
            MessageBusService.emitError("Nie wybrano dokumentu.");
            return;
        }

        this.fetching = true;
        this.recruitmentService.uploadDocument(this.file.name, this.file).then(this.updateLocals, this.handleError);
    }

    downloadDocument(fileId: number) {
        let doc = this.profile.documents.find(d => d.id == fileId);
        if (doc) {
            if (this.fetching)
                return;
            this.fetching = true;
            this.apiGateway.downloadDocument(fileId, doc.name).then(() => {
                this.fetching = false;
            }, err => {
                this.fetching = false;
                MessageBusService.emitError(getErrorMessage(err));
            });
        }
    }

    deleteDocument(fileId: number) {
        if (this.fetching)
            return;
        this.fetching = true;

        this.recruitmentService.deleteDocument(fileId).then(this.updateLocals, this.handleError);
    }

    updateLocals(resp: IProfileData) {
        this.fetching = false;

        this.profile = resp;
        this.birthDate = new Date(resp.birthDate).toLocaleDateString("pl");
        this.adress = resp.adress;
        this.fatherName = resp.fatherName;
        this.motherName = resp.motherName;
        this.primarySchool = resp.primarySchool;
        this.profilePicName = resp.profilePictureName;
        this.profilePictureFileId = resp.profilePictureFileId ? resp.profilePictureFileId : -1;
    }

    handleError(err: any) {
        this.fetching = false;
    }
}
