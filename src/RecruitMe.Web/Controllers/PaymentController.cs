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
        public async Task<ActionResult> AfterPayment([FromQuery] DotpayRedirectDto redirectDto)
        {
            // redirect to web or mobile, depending on data
            string redirectUrl = Get<EndpointConfig>().BaseAddress + "/payments/thankyou";

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
            // check what we received from Dotpay
            if (CheckPaymentResponse(response))
            {
                User user = await AuthenticateUser();

                //insert successful payment
                await Get<UpdateSuccessfulPaymentCommand>().Execute(response);

                //delete previously used link
                int userId = int.Parse(response.Control);
                int rows = await Get<RemoveExistingPaymentLink>().Execute(userId);
                if (rows != 1) throw new Exception($"Deleted different number of rows than one. Actual value: {rows}");

                //auto-assign candidate to all exams
                await Get<AssignCandidateToExamsCommand>().Execute(user);

                return Ok("OK");
            }

            return BadRequest();
        }

        private bool CheckPaymentResponse(PaymentResponseDto response)
        {
            // should check for signature also, but have no assurance that all properties in
            // PaymentResponseDto were hashed in response.Signature
            // so we check what we can
            return response.Type == OperationType.Payment &&
                response.Status == OperationStatus.Completed &&
                response.OperationAmount == response.OperationOriginalAmount &&
                response.OperationCurrency == response.OperationOriginalCurrency; //&&
                //response.Signature == 
        }
    }
}