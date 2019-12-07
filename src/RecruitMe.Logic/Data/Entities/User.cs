using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string  Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Pesel { get; set; }

        public string CandidateId { get; set; }

        public string PasswordHash { get; set; }

        public DateTime BirthDate { get; set; }

        public bool EmailVerified { get; set; }

        public virtual PersonalData PersonalData { get; set; }
        public virtual ConfirmationEmail ConfirmationEmail { get; set; }
        public virtual PasswordReset PasswordReset { get; set; }
        public virtual IEnumerable<PersonalDocument> PersonalDocuments { get; set; }
        public virtual IEnumerable<ExamTaker> ExamTakers { get; set; }
        

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Id == user.Id &&
                   (Email == user.Email || (Email is null && user.Email is null)) &&
                   (Name == user.Name || (Name is null && user.Name is null)) &&
                   (Surname == user.Surname || (Surname is null && user.Surname is null)) &&
                   (Pesel == user.Pesel || (Pesel is null && user.Pesel is null)) &&
                   (CandidateId == user.CandidateId || (CandidateId is null && user.CandidateId is null)) &&
                   (PasswordHash == user.PasswordHash || (PasswordHash is null && user.PasswordHash is null)) &&
                   BirthDate == user.BirthDate &&
                   EmailVerified == user.EmailVerified &&
                   EqualityComparer<PersonalData>.Default.Equals(PersonalData, user.PersonalData) &&
                   EqualityComparer<IEnumerable<PersonalDocument>>.Default.Equals(PersonalDocuments, user.PersonalDocuments);
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Id);
            hash.Add(Email);
            hash.Add(Name);
            hash.Add(Surname);
            hash.Add(Pesel);
            hash.Add(CandidateId);
            hash.Add(PasswordHash);
            hash.Add(BirthDate);
            hash.Add(EmailVerified);
            hash.Add(PersonalData);
            hash.Add(PersonalDocuments);
            return hash.ToHashCode();
        }
    }
}
