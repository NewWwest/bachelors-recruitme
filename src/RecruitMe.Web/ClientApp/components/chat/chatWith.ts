import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { UserService } from '../../services/user.service';
import { ApiGateway } from '../../api/api.gateway';

@Component({})
export default class ChatWithComponent extends Vue {
    userService: UserService = new UserService();
    apiGateway: ApiGateway = new ApiGateway();

}
