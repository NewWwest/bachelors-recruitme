import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../../services/user.service';
import { RecruitmentService } from '../../../services/recruitment.service';
import { ApiGateway } from '../../../api/api.gateway';

@Component({ })
export default class YourExamsComponent extends Vue {
    userService: UserService = new UserService();
    recruitmentService: RecruitmentService = new RecruitmentService();
    apiGateway: ApiGateway = new ApiGateway();
    
}
