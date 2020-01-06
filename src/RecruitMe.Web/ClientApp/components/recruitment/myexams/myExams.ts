import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../../services/user.service';
import { RecruitmentService } from '../../../services/recruitment.service';
import { ApiGateway } from '../../../api/api.gateway';
import { IMyExamsDto, IMyExamDto } from '../../../models/recruit.models';
import { toLocalTime } from '../../../helpers/datetime.helper';
import { ExamTypeDisplayName } from '../../../helpers/examType.helper';
import { getErrorMessage } from '../../../helpers/error.helper';
import { MessageBusService } from '../../../services/messageBus.service';

@Component({})
export default class MyExamsComponent extends Vue {
    userService: UserService = new UserService();
    recruitmentService: RecruitmentService = new RecruitmentService();
    apiGateway: ApiGateway = new ApiGateway();
    exams: IMyExamDto[] = [];
    examsFormatted: any[] = [];

    loaded: boolean = false;
    mounted() {
        this.recruitmentService.examsAndStatus().then(d => {
            this.exams = d.exams;
            this.examsFormatted = d.exams.map((e: IMyExamDto, i: number) => {
                let tempDate = toLocalTime(e.startTime);
                return {
                    durationInMinutes: e.durationInMinutes,
                    startDate: tempDate.toLocaleDateString("pl"),
                    startHour: tempDate.toLocaleTimeString("pl"),
                    categoryName: e.categoryName,
                    examTypeName: `Egzamin ${ExamTypeDisplayName(e.examType)}`,
                    key: i
                };
            });
            this.loaded = true;
        }, err => MessageBusService.emitError(getErrorMessage(err)));
    }
}
