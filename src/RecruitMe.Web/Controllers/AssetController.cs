using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Recruitment.ProfileFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RecruitMe.Web.Controllers
{
    [Route("api/asset/")]
    public class AssetController : RecruitMeBaseController
    {
        private readonly GetFileQuery _getFileQuery;

        public AssetController(GetFileQuery getFileQuery)
        {
            _getFileQuery = getFileQuery;
        }

        [HttpGet]
        [Route("{fileid}")]
        public async Task<ActionResult> GetProfilePicture(int fileid)
        {
            User user = await GetUser();
            var stream = await _getFileQuery.Execute(new GetFileQueryRequest()
            {
                FileId = fileid,
                UserId = user.Id,
            });
            return File(stream,"image/jpg");
        }
    }
}
