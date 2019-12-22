using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Payments;
using RecruitMe.Logic.Operations.Payments.Payment;
using RecruitMe.Logic.Operations.Payments.PaymentLink;

namespace RecruitMe.Web.Controllers
{
    [Route("api/payment/")]
    public class PaymentController : RecruitMeBaseController
    {
        public PaymentController() { }

        [HttpPost]
        [Route("processPayment")]
        public async Task<ActionResult> ProcessPayment([FromBody] PaymentDto paymentDto)
        {
            User user = await AuthenticateUser();
            paymentDto.Description = Get<GetNewPaymentDescriptionQuery>().Execute(user.Id);

            string redirectUrl = await Get<CreatePaymentLinkCommand>().Execute(paymentDto);
            return RedirectPermanent(redirectUrl);
        }

        // redirect after payment method
        [HttpGet]
        [Route("afterPayment")]
        public async Task<ActionResult> AfterPayment(dynamic data)
        {
            // redirect to web or mobile, depending on data
            string redirectUrl = "http://localhost:5000/payments/thankyou";

            return RedirectPermanent(redirectUrl);
        }

        // method for dotpay to call after successful money transfer
        [HttpPost]
        [Route("successfulMoneyTransfer")]
        public async Task<ActionResult> SuccessfulMoneyTransfer(dynamic data)
        {
            int userId = -1;

            //insert successful payment

            //delete previously used link
            int rows = await Get<RemoveExistingPaymentLink>().Execute(userId);

            if (rows != 1) throw new Exception($"Deleted different number of rows than one. Actual value: {rows}");

            return Ok();
        }
    }
}