using RecruitMe.Logic.Data.Entities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RecruitMe.Logic.Operations.Account.Helpers
{
    public class PasswordHasher
    {
        private static int HashIterations = 10000;
        private static int saltLength = 16;
        private static int hashLength = 20;


        public string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[saltLength]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, HashIterations);
            byte[] hash = pbkdf2.GetBytes(hashLength);

            byte[] hashBytes = new byte[hashLength + saltLength];
            Array.Copy(salt, 0, hashBytes, 0, saltLength);
            Array.Copy(hash, 0, hashBytes, saltLength, hashLength);

            return Convert.ToBase64String(hashBytes);
        }

        public bool VerifyPassword(User user, string password)
        {
            try
            {
                string savedPasswordHash = user.PasswordHash;
                byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
                byte[] salt = new byte[saltLength];

                Array.Copy(hashBytes, 0, salt, 0, saltLength);
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, HashIterations);
                byte[] hash = pbkdf2.GetBytes(hashLength);
                for (int i = 0; i < hashLength; i++)
                    if (hashBytes[i + saltLength] != hash[i])
                        return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
