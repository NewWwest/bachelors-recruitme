using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Configuration
{
    public class EmailContentConfiguration
    {
        public  static string EmailVerifiedTitle => 
            "Email został potwierdzony";

        public static string EmailVerifiedBody(string candidateID) =>
            $"Email został potwierdzony. Możesz się teraz zalogować do systemu używając loginu: {candidateID}";

        public static string RegisteredTitle =>
            "Dokończ rejestrację w systemie Recruit.Me";

        public static string RegisteredBody(string emailVerificationLink) =>
            $"Aby dokończyć rejestrację potwierdź swój email otwierając ten link: {emailVerificationLink}";

        public static string LoginRemindedTitle =>
            "Twój login w systemie Recruit.Me";

        public static string LoginRemindedBody(string login) =>
            $"Twój login w systemie Recruit.Me to: {login}";

        public static string ResetPasswordTitle =>
            "Zresetuj hasło w systemie Recruit.Me";

        public static string ResetPasswordBody(string passwordResetLink) =>
            $"Możesz zmienić swoje hasło w systemie klikając link: {passwordResetLink}";
    }
}
