using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Administration.Exam;
using RecruitMe.Logic.Operations.Payments;
using RecruitMe.Logic.Operations.Payments.Enums;
using RecruitMe.Logic.Operations.Payments.Payment;
using RecruitMe.Logic.Operations.Payments.PaymentLink;
using RecruitMe.Logic.Operations.Payments.SuccessfulMoneyTransfer;

namespace RecruitMe.Web.Controllers
{
    [Route("api/payment/")]
    public class PaymentController : RecruitMeBaseController
    {
        public PaymentController() { }

        [HttpPost]
        [Route("processPayment")]
        public async Task<ActionResult> ProcessPayment([FromBody] PayerDto payerDto)
        {
            User user = await AuthenticateUser();

            Payment payment = await Get<InsertNewPaymentCommand>().Execute(user);

            PaymentDto paymentDto = new PaymentDto
            {
                Description = payment.Description,
                Control = $"{payment.Id}:{payment.UserId}"
            };
            paymentDto.SetPayerAndUrls(payerDto, Get<EndpointConfig>());
            paymentDto.SetPaymentsConfiguration(Get<PaymentConfiguration>());

            string redirectUrl = await Get<CreatePaymentLinkCommand>().Execute(paymentDto);
            return Json(redirectUrl);
        }

        // redirect after payment method
        [HttpGet]
        [Route("afterPayment")]
        public ActionResult AfterPayment([FromQuery] DotpayRedirectDto redirectDto)
        {
            // redirect to web or mobile, depending on data
            string err = "";
            if ("OK" != redirectDto.StatusCode?.ToUpperInvariant())
                err = redirectDto.ErrorCode.ToString();

            string redirectUrl = Get<EndpointConfig>().PaymentsThankYou(err);

            if (IsMobileBrowser())
            {
                redirectUrl = "recruitme://" + redirectUrl;
            }

            return RedirectPermanent(redirectUrl);
        }

        // method for dotpay to call after successful money transfer
        [HttpPost]
        [Route("successfulMoneyTransfer")]
        public async Task<ActionResult> SuccessfulMoneyTransfer([FromQuery] PaymentResponseDto response)
        {
            await Get<SuccessfulMoneyTransferCommand>().Execute(response);

            return Ok("OK");
        }

        [HttpGet]
        [Route("isPaymentDone")]
        public async Task<ActionResult> IsPaymentDone()
        {
            User user = await AuthenticateUser();
            bool result = await Get<IsPaymentDoneQuery>().Execute(user);

            return Json(result ? 1 : 0);
        }
    }
}