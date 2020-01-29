using System;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Account.Login;

namespace RecruitMe.Web.Services
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly LoginUserQuery _loginUserQuery;
        private readonly ILogger _logger;

        public CustomResourceOwnerPasswordValidator(LoginUserQuery loginUserQuery, ILogger logger)
        {
            _loginUserQuery = loginUserQuery;
            _logger = logger;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            User user = null;
            try
            {
                user = await _loginUserQuery.Execute(new LoginDto() { CandidateId = context.UserName, Password = context.Password });
                context.Result = new GrantValidationResult(user.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
                return;
            }
            catch(Exception e)
            {
                _logger.Log(e);
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Validation Error");
            }
        }
    }
}
