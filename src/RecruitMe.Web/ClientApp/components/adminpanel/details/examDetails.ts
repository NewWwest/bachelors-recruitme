import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import { ApiGateway } from '../../../api/api.gateway';
import { IExamCategory, IExamTaker, IExam, ITeacher } from '../../../models/administraion.models';
import { toLocalTime } from '../../../helpers/datetime.helper';

@Component({})
export default class ExamDetails extends Vue {
    apiGateway: ApiGateway = new ApiGateway();

    @Prop()
    id: number | undefined;
    exam: IExam = {} as IExam;
    examCategories: IExamCategory[] = [];
    examTakers: IExamTaker[] = []
    teachers: any[] = []

    ratingTeacher: number = 0;
    processingOmr: boolean = false;

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
            this.apiGateway.listTeachers().then(resp => {
                console.log(resp)
                this.teachers = resp.map((t: ITeacher) => {
                    id: t.id;
                    name: t.name + " " + t.surname;
                });
                console.log(this.teachers)
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

    downloadExamSheet() {
        if (this.id) {
            this.apiGateway.downloadExamSheet(this.id);
        }
    }

    uploadExamSheet(evt: any) {
        if (this.id) {
            this.processingOmr = true;
            let file = evt.target.files[0];
            this.apiGateway.uploadExamSheet(this.id, this.ratingTeacher, file).then(r => {
                this.apiGateway.getExam(this.id as number).then(resp => {
                    this.exam = resp.exam;
                    this.exam.startDateTime = toLocalTime(this.exam.startDateTime);
                    this.setExamTakers(resp.examTakers)
                    this.processingOmr = false;
                }, err => {
                    this.processingOmr = false;
                    console.error(err);
                });
            }, err => {
                this.processingOmr = false;
                console.error(err);
            });
        }

    }
}
