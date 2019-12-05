import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { SystemEntity, ITeacher, IExam, IExamCategory, ExamType } from '../../../models/administraion.models';

@Component({})
export default class AddComponent extends Vue {
    SystemEntityEnum = SystemEntity;
    ExamTypeEnum = ExamType;
    currentSystemEntity: SystemEntity = SystemEntity.Candidate;

    teacher: ITeacher = {} as ITeacher
    exam: IExam = {} as IExam;
    examCategory: IExamCategory = {} as IExamCategory;
    examCategories: IExamCategory[] = [];

    examTypes: any[] = [{
            name: "Indywidualny",
            id: ExamType.Individual
        }, {
            name: "Pisemny",
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
                console.error("SystemEntity.ExamCategory, TODO: implement api saving");
                this.$router.push(`/adminPanel/manage/${SystemEntity.ExamCategory}`);
                break;

            case SystemEntity.Teacher:
                console.error("SystemEntity.teacher, TODO: implement api saving");
                this.$router.push(`/adminPanel/manage/${SystemEntity.Teacher}`);
                break;
        }
    }
}
