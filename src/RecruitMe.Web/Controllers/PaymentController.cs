using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        private readonly CreatePaymentLinkCommand _createPaymentLinkCommand;
        private readonly GetNewPaymentDescriptionQuery _getNewPaymentDescriptionQuery;

        public PaymentController(
            CreatePaymentLinkCommand createPaymentLinkCommand,
            GetNewPaymentDescriptionQuery getNewPaymentDescriptionQuery)
        {
            _createPaymentLinkCommand = createPaymentLinkCommand;
            _getNewPaymentDescriptionQuery = getNewPaymentDescriptionQuery;
        }

        [HttpPost]
        [Route("processPayment")]
        public async Task<ActionResult> ProcessPayment([FromBody] PaymentDto paymentDto)
        {
            User user = await GetUser();
            paymentDto.Description = _getNewPaymentDescriptionQuery.Execute(user.Id);

            string redirectUrl = await _createPaymentLinkCommand.Execute(paymentDto);
            return RedirectPermanent(redirectUrl);
        }

        // redirect after payment method

        // method for dotpay to call after successful money transfer
        [HttpPost]
        [Route("successfulMoneyTransfer")]
        public async Task<ActionResult> SuccessfulMoneyTransfer(object data)
        {
            return Ok();
        }
    }
}