import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import { ApiGateway } from '../../../api/api.gateway';
import { IProfileData, RecruitmentStatus } from '../../../models/recruit.models';
import { SystemEntity, IExam, IExamTaker, ITeacher } from '../../../models/administraion.models';
import { toLocalTime } from '../../../helpers/datetime.helper';
import { MessageBusService } from '../../../services/messageBus.service';
import { getErrorMessage } from '../../../helpers/error.helper';

@Component({})
export default class CandidateDetailsComponent extends Vue {
    RecruitmentStatusEnum = RecruitmentStatus;
    apiGateway = new ApiGateway();
    @Prop()
    candidateId: number | undefined;

    profile: IProfileData = {} as IProfileData;
    userExams: IExamTaker[] = [];
    exams: any[] = [];
    examsData: any[] = [];
    teachers: any[] = [];
    newExamTaker: IExamTaker = {} as IExamTaker;;

    menu: boolean = false;
    submitted: boolean = false;
    fetching: boolean = false;
    registrationCompleted: boolean = false;
    backendError: string = "";

    mounted() {
        if (this.candidateId) {
            this.fetchProfile();
        }
    }

    fetchProfile() {
        this.apiGateway.getProfile(this.candidateId as number).then(
            d => this.setprofile(d),
            err => MessageBusService.emitError(getErrorMessage(err)));
        this.apiGateway.listExamsForUser(this.candidateId as number).then(d => {
            this.setUserExams(d);
        }, err => MessageBusService.emitError(getErrorMessage(err)));
        this.apiGateway.listExams().then(d => {
            this.exams = d.map((e: IExam) => {
                return {
                    id: e.id,
                    name: e.examCategoryName + " @" + e.startDateTime,
                    startDateTime: new Date(e.startDateTime)
                }
            })
        }, err => MessageBusService.emitError(getErrorMessage(err)));
        this.apiGateway.listTeachers().then(d => {
            this.teachers = d.map((e: ITeacher) => {
                return {
                    id: e.id,
                    name: e.name + " " + e.surname
                }
            })
        }, err => MessageBusService.emitError(getErrorMessage(err)));
    }

    handleSubmit() {
        this.apiGateway.updateUser(this.profile).then(
            d => this.setprofile(d),
            err => MessageBusService.emitError(getErrorMessage(err)));
    }

    getIdentityCard() {
        this.apiGateway.downloadIdCard(this.profile.userId).then(d => {
        }, err => MessageBusService.emitError(getErrorMessage(err)));
    }

    handleDelete() {
        this.apiGateway.deleteCandidate(this.profile.userId).then(d => {
            this.$router.push(`/adminPanel/manage/${SystemEntity.Candidate}`);
        }, err => MessageBusService.emitError(getErrorMessage(err)));
    }

    deleteDocument(fileId: number) {
        this.apiGateway.deleteDocument(fileId, this.profile.userId).then(
            d => this.setprofile(d.data),
            err => MessageBusService.emitError(getErrorMessage(err)));
    }

    downloadDocument(fileId: number) {
        if (fileId == this.profile.profilePictureFileId) {
            this.apiGateway.downloadDocument(fileId, this.profile.profilePictureName, this.profile.userId).catch(err => MessageBusService.emitError(getErrorMessage(err)));

        } else {
            let doc = this.profile.documents.find(d => d.id == fileId);
            if (doc) {
                this.apiGateway.downloadDocument(fileId, doc.name, this.profile.userId).catch(err => MessageBusService.emitError(getErrorMessage(err)));
            }
        }
    }

    setDefaultDate() {
        let matches = this.exams.filter(e => e.id == this.newExamTaker.examId)
        if (matches && matches.length > 0) {
            this.newExamTaker.startDate = matches[0].startDateTime;
        }
    }

    addExamTaker() {
        this.newExamTaker.candidateId = this.profile.candidateId;
        this.newExamTaker.userId = this.profile.userId;

        this.apiGateway.addOrUpdateExamTaker(this.newExamTaker).then(d => {
            this.setUserExams(d);
        }, err => MessageBusService.emitError(getErrorMessage(err)));
    }

    saveExamTaker(data: IExamTaker) {
        this.apiGateway.addOrUpdateExamTaker(data).then(d => {
            this.setUserExams(d);
        }, err => MessageBusService.emitError(getErrorMessage(err)));
    }

    removeExamTaker(data: IExamTaker) {
        this.apiGateway.deleteExamTaker(this.profile.userId, data.id).then(d => {
            this.setUserExams(d);
        }, err => MessageBusService.emitError(getErrorMessage(err)));
    }

    setprofile(newProfile: IProfileData) {
        this.profile = newProfile;
        if (!this.profile.status) {
            this.profile.status = 0;
        }
    }
    setUserExams(data: IExamTaker[]) {
        this.newExamTaker = {} as IExamTaker;
        this.userExams = data;
        for (let i = 0; i < this.userExams.length; i++) {
            this.userExams[i].startDate = toLocalTime(this.userExams[i].startDate);
        }
    }
}
