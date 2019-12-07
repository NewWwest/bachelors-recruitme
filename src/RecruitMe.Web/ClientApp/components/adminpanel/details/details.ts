import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { SystemEntity, ITeacher, IExam, IExamCategory, ExamType, ExamTypeDisplayName } from '../../../models/administraion.models';
import { ApiGateway } from '../../../api/api.gateway';

@Component({
    components: {
        "exam-details": require('./examDetails').default,
        "candidate-details": require('./candidateDetails').default
    }
})
export default class DetailsComponent extends Vue {
    apiGateway = new ApiGateway();
    SystemEntityEnum = SystemEntity;
    ExamTypeEnum = ExamType;
    
    currentSystemEntity: SystemEntity = SystemEntity.Candidate;
    id: number = 0;

    teacher: ITeacher = {} as ITeacher
    exam: IExam = {} as IExam;
    examCategory: IExamCategory = {} as IExamCategory;
    examCategories: IExamCategory[] = [];
    examTypes: any[] = [{
        name: ExamTypeDisplayName(ExamType.Individual),
        id: ExamType.Individual
    }, {
        name: ExamTypeDisplayName(ExamType.Collective),
        id: ExamType.Collective
    }
    ];


    mounted() {
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

        this.id = id;
        this.currentSystemEntity = type as SystemEntity;
        this.fetchItem();
    }

    handleDelete() {
        switch (this.currentSystemEntity) {
            case SystemEntity.ExamCategory:
                this.apiGateway.deleteExamCategory(this.id).then((resp: any) => {
                    this.$router.push(`/adminPanel/manage/${SystemEntity.ExamCategory}`);
                }, (err: any) => {
                    console.error(err)
                });
                break;

            case SystemEntity.Teacher:
                this.apiGateway.deleteTeacher(this.id).then((resp: any) => {
                    this.$router.push(`/adminPanel/manage/${SystemEntity.Teacher}`);
                }, (err: any) => {
                    console.error(err)
                });
                break;
        }
    }

    handleSubmit() {
        switch (this.currentSystemEntity) {
            case SystemEntity.ExamCategory:
                this.apiGateway.updateExamCategory(this.examCategory).then((resp: any) => {
                    this.$router.push(`/adminPanel/manage/${SystemEntity.ExamCategory}`);
                }, (err: any) => {
                    console.error(err)
                });
                break;

            case SystemEntity.Teacher:
                this.apiGateway.updateTeacher(this.teacher).then((resp: any) => {
                    this.$router.push(`/adminPanel/manage/${SystemEntity.Teacher}`);
                }, (err: any) => {
                    console.error(err)
                });
                break;
        }
    }
    fetchItem() {
        switch (this.currentSystemEntity) {
            case SystemEntity.ExamCategory:
                this.apiGateway.getExamCategory(this.id).then((resp: IExamCategory) => {
                    this.examCategory = resp;
                }, (err: any) => {
                    console.error(err)
                });
                break;

            case SystemEntity.Teacher:
                this.apiGateway.getTeacher(this.id).then((resp: ITeacher) => {
                    this.teacher = resp;
                }, (err: any) => {
                    console.error(err)
                });
                break;
        }
    }
}
