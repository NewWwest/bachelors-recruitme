import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { SystemEntity, ITeacher, IExam, IExamCategory, ExamType } from '../../../models/administraion.models';
import { ApiGateway } from '../../../api/api.gateway';
import { UserService } from '../../../services/user.service';
import { ExamTypeDisplayName } from '../../../helpers/examType.helper';

@Component({})
export default class AddComponent extends Vue {
    apiGateway = new ApiGateway();
    userService = new UserService();
    SystemEntityEnum = SystemEntity;
    ExamTypeEnum = ExamType;
    currentSystemEntity: SystemEntity = SystemEntity.Candidate;

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
        if (!this.userService.isAdmin()) {
            this.$router.push("/");
        }

        let sysEntitytype = this.$route.params.entityType;
        if (sysEntitytype != SystemEntity.Exam &&
            sysEntitytype != SystemEntity.ExamCategory &&
            sysEntitytype != SystemEntity.Teacher) {
            this.$router.push(`/adminPanel/manage/${SystemEntity.Candidate}`);
        }

        this.currentSystemEntity = sysEntitytype as SystemEntity;
        if (this.currentSystemEntity == SystemEntity.Exam) {
            this.apiGateway.listExamCategories().then(resp => {
                this.examCategories = resp;
            }, err => {
                console.error(err);
            });
        }
    }

    handleSubmit() {
        switch (this.currentSystemEntity) {
            case SystemEntity.Exam:
                this.apiGateway.addExam(this.exam).then(resp => {
                    this.$router.push(`/adminPanel/manage/${SystemEntity.Exam}`);
                }, err => {
                    console.error(err)
                });
                break;

            case SystemEntity.ExamCategory:
                this.apiGateway.addExamCategory(this.examCategory).then(resp => {
                    this.$router.push(`/adminPanel/manage/${SystemEntity.ExamCategory}`);
                }, err => {
                    console.error(err)
                });
                break;

            case SystemEntity.Teacher:
                this.apiGateway.addTeacher(this.teacher).then(resp => {
                    this.$router.push(`/adminPanel/manage/${SystemEntity.Teacher}`);
                }, err => {
                    console.error(err)
                });
                break;
        }
    }
}
