import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { SystemEntity, ITeacher, IExam, IExamCategory, ExamType, ExamTypeDisplayName } from '../../../models/administraion.models';
import { ApiGateway } from '../../../api/api.gateway';

@Component({})
export default class AddComponent extends Vue {
    apiGateway = new ApiGateway();
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
        let type = this.$route.params.entityType;
        if (type != SystemEntity.Exam &&
            type != SystemEntity.ExamCategory &&
            type != SystemEntity.Teacher) {
            this.$router.push(`/adminPanel/manage/${SystemEntity.Candidate}`);
        }

        this.currentSystemEntity = type as SystemEntity;
    }
    
    handleSubmit() {
        switch (this.currentSystemEntity) {
            case SystemEntity.Exam:
                console.error("SystemEntity.Exam, TODO: implement api saving");
                this.$router.push(`/adminPanel/manage/${SystemEntity.Exam}`);
                break;

            case SystemEntity.ExamCategory:
                console.log(this.examCategory);
                this.apiGateway.addExamCategory(this.examCategory).then(resp => {
                        this.$router.push(`/adminPanel/manage/${SystemEntity.ExamCategory}`);
                    },
                    err => {
                        console.error(err)
                    });
                break;

            case SystemEntity.Teacher:
                console.error("SystemEntity.teacher, TODO: implement api saving");
                this.$router.push(`/adminPanel/manage/${SystemEntity.Teacher}`);
                break;
        }
    }
}
