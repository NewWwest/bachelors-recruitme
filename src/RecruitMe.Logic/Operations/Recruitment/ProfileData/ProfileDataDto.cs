using RecruitMe.Logic.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecruitMe.Logic.Operations.Recruitment.ProfileData
{
    public class ProfileDataDto
    {
        public string Adress { get; set; }

        public string MotherName { get; set; }

        public string FatherName { get; set; }

        public string PrimarySchool { get; set; }

        public string ProfilePictureName { get; set; }

        public int? ProfilePictureFileId { get; set; }

        public List<DocumentDto> Documents { get; set; }

        public static ProfileDataDto FromPersonalDataEntity(PersonalData entity, IEnumerable<PersonalDocument> documents)
        {
            var docs = documents?
                .Where(d => d.Id != entity.ProfilePictureFileId)
                .Select(d => new DocumentDto()
                {
                    FileUrl = d.FileUrl,
                    Id = d.Id,
                    Name = d.Name,
                    ContentType = d.ContentType
                }).ToList() ?? new List<DocumentDto>();

            var result = new ProfileDataDto()
            {
                Adress = entity?.Adress,
                FatherName = entity?.FatherName,
                MotherName = entity?.MotherName,
                PrimarySchool = entity?.PrimarySchool,
                ProfilePictureFileId = entity?.ProfilePictureFileId,
                ProfilePictureName = documents.FirstOrDefault(d => d.Id == entity?.ProfilePictureFileId)?.Name,
                Documents = docs
            };
            return result;
        }
    }
}
