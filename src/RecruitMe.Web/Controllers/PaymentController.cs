using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Configuration;
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
        public async Task<ActionResult> ProcessPayment([FromBody] PayerDto payerDto)
        {
            User user = await AuthenticateUser();

            PaymentDto paymentDto = new PaymentDto();
            paymentDto.Description = Get<GetNewPaymentDescriptionQuery>().Execute(user.Id);
            paymentDto.SetPayerAndUrls(payerDto, Get<EndpointConfig>());

            string redirectUrl = await Get<CreatePaymentLinkCommand>().Execute(paymentDto);
            return Json(redirectUrl);
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
        public async Task<ActionResult> SuccessfulMoneyTransfer([FromQuery] PaymentResponseDto response)
        {
            //insert successful payment
            await Get<UpdateSuccessfulPaymentCommand>().Execute(response);

            //delete previously used link
            int userId = int.Parse(response.Control);
            int rows = await Get<RemoveExistingPaymentLink>().Execute(userId);

            if (rows != 1) throw new Exception($"Deleted different number of rows than one. Actual value: {rows}");

            return Ok();
        }
    }
}