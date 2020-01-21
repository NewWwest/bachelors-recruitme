using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Abstractions;
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
        [AllowAnonymous]
        [Route("afterPayment")]
        public ActionResult AfterPayment([FromQuery] DotpayRedirectDto redirectDto)
        {
            // redirect to web or mobile, depending on data
            string err = "";
            if ("OK" != redirectDto.status?.ToUpperInvariant())
                err = redirectDto.error_code.ToString();

            string redirectUrl = Get<EndpointConfig>().PaymentsThankYou(err);

            if (IsMobileBrowser())
            {
                UriBuilder uriBuilder = new UriBuilder(redirectUrl)
                {
                    Scheme = "recruitme",
                    Port = -1
                };
                redirectUrl = uriBuilder.Uri.ToString();

                // hack for not getting parameters in urlhandler in mobile app
                if (redirectUrl.Contains('?'))
                {
                    redirectUrl = redirectUrl.Replace('?', '/').Replace('=', '/');
                }
            }

            return RedirectPermanent(redirectUrl);
        }

        // method for dotpay to call after successful money transfer
        [HttpPost]
        [Route("successfulMoneyTransfer")]
        public async Task<ActionResult> SuccessfulMoneyTransfer([FromQuery] PaymentResponseDto response)
        {
            OperationResult result = await Get<SuccessfulMoneyTransferCommand>().Execute(response);

            if (result.Success)
                return Ok("OK");
            else return BadRequest("Operation failed");
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