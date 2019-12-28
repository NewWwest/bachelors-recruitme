import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { PaymentsService } from '../../../services/payments.service';

@Component({ })
export default class MakePayment extends Vue {
    intervalId: any;
    firstTimeFetch: boolean = true;
    isPaymentMade: boolean = false;
    paymentService: PaymentsService = new PaymentsService();

    constructor() {
        super();
        this.intervalId = setInterval(this.paymentCheck, 30000);
    }

    beforeMount() {
        this.paymentCheck();
    }

    makePayment() {
        this.paymentService.makePayment().then(href => {
            if (new URL(href)) {
                // should throw an exception if not valid url
                window.location.href = href;
            }
        })
    }

    paymentCheck() {
        this.paymentService.isPaymentDone().then(isDone => {
            this.isPaymentMade = isDone;
            this.firstTimeFetch = false;

            if (isDone) {
                clearInterval(this.intervalId);
            }
        })
    }
}
