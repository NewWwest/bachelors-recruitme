import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import { ApiGateway } from '../../../api/api.gateway';
import { IExamCategory, IExamTaker, IExam } from '../../../models/administraion.models';
import { toLocalTime } from '../../../helpers/datetime.helper';

@Component({})
export default class ExamDetails extends Vue {
    apiGateway: ApiGateway = new ApiGateway();

    @Prop()
    id: number | undefined;
    exam: IExam = {} as IExam;
    examCategories: IExamCategory[] = [];
    examTakers: IExamTaker[] = []

    mounted() {
        if (this.id) {
            this.apiGateway.getExam(this.id).then(resp => {
                this.exam = resp.exam;
                this.exam.startDateTime = toLocalTime(this.exam.startDateTime);
                this.setExamTakers(resp.examTakers)
            }, err => {
                console.error(err);
            });
            this.apiGateway.listExamCategories().then(resp => {
                this.examCategories = resp;
            }, err => {
                console.error(err);
            });
        }
    }
    handleDelete() {
        if (!this.id)
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

    deleteUserTaker(userId: number, id: number) {
        this.apiGateway.deleteExamTaker(userId, id).then((resp: any) => {
            this.setExamTakers(resp)
        }, (err: any) => {
            console.error(err)
        });
    }
    setExamTakers(examTakers: IExamTaker[]) {
        this.examTakers = examTakers;
        for (let i = 0; i < examTakers.length; i++) {
            this.examTakers[i].startDate = toLocalTime(this.examTakers[i].startDate);
        }
    }
}
