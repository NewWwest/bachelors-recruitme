import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { PaymentsService } from '../../../services/payments.service';

@Component({ })
export default class MakePayment extends Vue {
    paymentService: PaymentsService = new PaymentsService();


    makePayment() {
        this.paymentService.makePayment();
    }
}
