import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import { ApiGateway } from '../../../api/api.gateway';
import { IExamCategory, IExamTaker, IExam } from '../../../models/administraion.models';

@Component({})
export default class ExamDetails extends Vue {
    apiGateway: ApiGateway = new ApiGateway();

    @Prop()
    id: number | undefined;
    exam: IExam = { } as IExam;
    examCategories: IExamCategory[] = [];
    examTakers: IExamTaker[] = []

    mounted() {
        if (this.id) {
            this.apiGateway.getExam(this.id).then(resp => {
                this.exam = resp.exam;
                this.exam.startDateTime = new Date(this.exam.startDateTime);
                this.examTakers = resp.examTakers;
            }, err => {
                console.log(err);
            });
            this.apiGateway.listExamCategories().then(resp => {
                this.examCategories = resp;
            }, err => {
                console.log(err);
            });
        }
    }
    handleDelete() {
        if(!this.id)
            return;

        this.apiGateway.deleteExam(this.id).then((resp: any) => {
            this.$router.push(`/adminPanel/manage/exam`);
        }, (err: any) => {
            console.error(err)
        });
    }

    handleSubmit() {
        if (!this.id)
            return;

        this.apiGateway.updateExam(this.exam).then((resp: any) => {
            this.$router.push("/adminPanel/manage/exam");
        }, (err: any) => {
            console.error(err)
        });
    }
}
