using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Account;
using RecruitMe.Web.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecruitMe.Web.Services
{
    public class CustomProfileService : IProfileService
    {
        private readonly GetUserQuery _getUserQuery;
        private readonly ILogger _logger;

        public CustomProfileService(GetUserQuery getUserQuery, ILogger logger)
        {
            _getUserQuery = getUserQuery;
            _logger = logger;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string idStr = context.Subject.GetSubjectId();
            var id = int.Parse(idStr);

            User user = await _getUserQuery.Execute(id);
            var claims = new List<Claim>
            {
                new Claim(JwtClaims.ClaimUserId, user.Id.ToString()),
                new Claim(JwtClaims.ClaimName, user.Name.ToString()),
                new Claim(JwtClaims.ClaimSurname, user.Surname.ToString()),
                new Claim(JwtClaims.ClaimEmail, user.Email.ToString())
            };

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                var idStr = context.Subject.GetSubjectId();
                var id = int.Parse(idStr);
                await _getUserQuery.Execute(id);
                context.IsActive = true;
            }
            catch (Exception e)
            {
                _logger.Log(e);
                context.IsActive = false;
            }
        }
    }
}
