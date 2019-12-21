using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Configuration
{
    public class EndpointConfig : IAutoComponent
    {
        public EndpointConfig(BusinessConfiguration businessConfiguration)
        {
            BaseAddress = businessConfiguration.BaseAddress;
        }

        public string BaseAddress { get; private set; }
        public string ConfirmEmail(string guid) => BaseAddress + $"/api/account/confirmEmail/{guid}";
        public string SetNewPassword(string id) => BaseAddress + $"/account/SetNewPassword?token={id}";
        public string EmailVerified(string candidateId) => BaseAddress + $"/account/EmailVerified?candidateId={candidateId}";
    }
}
