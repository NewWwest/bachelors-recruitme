using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Account.Queries;

namespace RecruitMe.Web.Services
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly LoginUserQuery _loginUserQuery;

        public CustomResourceOwnerPasswordValidator(LoginUserQuery loginUserQuery)
        {
            _loginUserQuery = loginUserQuery;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            User user = null;
            try
            {
                user = await _loginUserQuery.Execute(new Logic.Operations.Account.Dto.LoginDto() { CandidateId = context.UserName, Password = context.Password });
                context.Result = new GrantValidationResult(user.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
                return;
            }
            catch(Exception e)
            {
                //log e
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Validation Error");
            }
        }
    }
}
