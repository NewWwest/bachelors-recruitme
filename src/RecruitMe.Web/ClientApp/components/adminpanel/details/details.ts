import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { SystemEntity, ITeacher, IExam, IExamCategory, ExamType } from '../../../models/administraion.models';
import { ApiGateway } from '../../../api/api.gateway';
import { ExamTypeDisplayName } from '../../../helpers/examType.helper';
import { MessageBusService } from '../../../services/messageBus.service';
import { getErrorMessage } from '../../../helpers/error.helper';

@Component({
    components: {
        "exam-details": require('./examDetails.vue.html').default,
        "candidate-details": require('./candidateDetails.vue.html').default
    }
})
export default class DetailsComponent extends Vue {
    apiGateway = new ApiGateway();
    SystemEntityEnum = SystemEntity;

    currentSystemEntity: SystemEntity = SystemEntity.Candidate;
    entityId: number = 0;

    teacher: ITeacher = {} as ITeacher;
    examCategory: IExamCategory = {} as IExamCategory;
    examTypes: any[] = [{
        name: ExamTypeDisplayName(ExamType.Individual),
        id: ExamType.Individual
    }, {
        name: ExamTypeDisplayName(ExamType.Collective),
        id: ExamType.Collective
    }
    ];


    beforeMount() {
        let type = this.$route.params.entityType;
        if (type != SystemEntity.Exam &&
            type != SystemEntity.ExamCategory &&
            type != SystemEntity.Teacher &&
            type != SystemEntity.Candidate) {
            this.$router.push(`/adminPanel/manage/${SystemEntity.Candidate}`);
        }
        let id = parseInt(this.$route.params.id);
        if (!id || id <= 0) {
            this.$router.push(`/adminPanel/manage/${SystemEntity.Candidate}`);
        }

        this.entityId = id;
        this.currentSystemEntity = type as SystemEntity;
    }

    mounted() {
        this.fetchItem();
    }

    handleDelete() {
        switch (this.currentSystemEntity) {
            case SystemEntity.ExamCategory:
                this.apiGateway.deleteExamCategory(this.entityId).then((resp: any) => {
                    this.$router.push(`/adminPanel/manage/${SystemEntity.ExamCategory}`);
                }, err => MessageBusService.emitError(getErrorMessage(err)));
                break;
            case SystemEntity.Teacher:
                this.apiGateway.deleteTeacher(this.entityId).then((resp: any) => {
                    this.$router.push(`/adminPanel/manage/${SystemEntity.Teacher}`);
                }, err => MessageBusService.emitError(getErrorMessage(err)));
                break;
        }
    }

    handleSubmit() {
        switch (this.currentSystemEntity) {
            case SystemEntity.ExamCategory:
                this.apiGateway.updateExamCategory(this.examCategory).then((resp: any) => {
                    this.$router.push(`/adminPanel/manage/${SystemEntity.ExamCategory}`);
                }, err => MessageBusService.emitError(getErrorMessage(err)));
                break;
            case SystemEntity.Teacher:
                this.apiGateway.updateTeacher(this.teacher).then((resp: any) => {
                    this.$router.push(`/adminPanel/manage/${SystemEntity.Teacher}`);
                }, err => MessageBusService.emitError(getErrorMessage(err)));
                break;
        }
    }

    fetchItem() {
        switch (this.currentSystemEntity) {
            case SystemEntity.ExamCategory:
                this.apiGateway.getExamCategory(this.entityId).then((resp: IExamCategory) => {
                    this.examCategory = resp;
                }, err => MessageBusService.emitError(getErrorMessage(err)));
                break;
            case SystemEntity.Teacher:
                this.apiGateway.getTeacher(this.entityId).then((resp: ITeacher) => {
                    this.teacher = resp;
                }, err => MessageBusService.emitError(getErrorMessage(err)));
                break;
        }
    }
}
