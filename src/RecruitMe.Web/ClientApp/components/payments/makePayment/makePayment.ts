import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { PaymentsService } from '../../../services/payments.service';

@Component({ })
export default class MakePayment extends Vue {
    paymentService: PaymentsService = new PaymentsService();

    makePayment() {
        this.paymentService.makePayment().then(href => {
            if (new URL(href)) {
                // should throw an exception if not valid url
                window.location.href = href;
            }
        })
    }
}
