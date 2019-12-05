import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { SystemEntity, ITeacher, IExam, IExamCategory, ExamType } from '../../../models/administraion.models';

@Component({
    components: {
        "exam-details": require('./examDetails').default,
        "candidate-details": require('./candidateDetails').default
    }
})
export default class DetailsComponent extends Vue {
    SystemEntityEnum = SystemEntity;
    ExamTypeEnum = ExamType;
    
    currentSystemEntity: SystemEntity = SystemEntity.Candidate;
    id: number | null = null;

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
            type != SystemEntity.Teacher &&
            type != SystemEntity.Candidate) {
            this.$router.push(`/adminPanel/manage/${SystemEntity.Candidate}`);
        }
        console.log("XD");
        console.log(this.$route.params.id)
        let id = parseInt(this.$route.params.id);
        if (!id || id <= 0) {
            this.$router.push(`/adminPanel/manage/${SystemEntity.Candidate}`);
        }

        this.id = id;
        this.currentSystemEntity = type as SystemEntity;
    }

    handleDelete() {
        switch (this.currentSystemEntity) {
            case SystemEntity.ExamCategory:
                console.error("SystemEntity.ExamCategory, TODO: implement api DELETE");
                this.$router.push(`/adminPanel/manage/${SystemEntity.ExamCategory}`);
                break;

            case SystemEntity.Teacher:
                console.error("SystemEntity.Teacher, TODO: implement api DELETE");
                this.$router.push(`/adminPanel/manage/${SystemEntity.Teacher}`);
                break;
        }
    }

    handleSubmit() {
        switch (this.currentSystemEntity) {
            case SystemEntity.ExamCategory:
                console.error("SystemEntity.ExamCategory, TODO: implement api update");
                this.$router.push(`/adminPanel/manage/${SystemEntity.ExamCategory}`);
                break;

            case SystemEntity.Teacher:
                console.error("SystemEntity.ExamCategory, TODO: implement api update");
                this.$router.push(`/adminPanel/manage/${SystemEntity.Teacher}`);
                break;
        }
    }
}
