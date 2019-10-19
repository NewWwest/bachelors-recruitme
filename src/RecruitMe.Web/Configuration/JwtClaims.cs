using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Web.Configuration
{
    public class JwtClaims
    {
        public static readonly string ClaimId = "id";
        public static readonly string ClaimName = "name";
        public static readonly string ClaimSurname = "surname";
        public static readonly string ClaimEmail = "email";
        public static readonly string ClaimPesel = "pesel";
    }
}
