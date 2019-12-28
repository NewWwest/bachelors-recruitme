import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../../services/user.service';
import { RecruitmentService } from '../../../services/recruitment.service';
import { ApiGateway } from '../../../api/api.gateway';
import { IExamDataDto, RecrutationStatus } from '../../../models/recruit.models';
import { toLocalTime } from '../../../helpers/datetime.helper';
import { ExamTypeDisplayName } from '../../../helpers/examType.helper';

@Component({ })
export default class YourExamsComponent extends Vue {
    RecrutationStatusEnum = RecrutationStatus;
    userService: UserService = new UserService();
    recruitmentService: RecruitmentService = new RecruitmentService();
    apiGateway: ApiGateway = new ApiGateway();
    exams: IExamDataDto[] = [];
    examsFormatted: any[] = [];
    status: RecrutationStatus | undefined;


    mounted() {
        this.recruitmentService.examsAndStatus().then(d => {
            this.exams = d.exams;
            this.status = d.status;
            this.examsFormatted = d.exams.map(e => {
                let tempDate = toLocalTime(e.startTime);
                return {
                    durationInMinutes: e.durationInMinutes,
                    startDate: tempDate.toLocaleDateString("pl"),
                    startHour: tempDate.toLocaleTimeString("pl"),
                    categoryName: e.categoryName;
                    examTypeName: `Egzamin ${ExamTypeDisplayName(e.examType)}`;
                };
            });
        }, err => {
            console.error(err);
        });
    }
}
