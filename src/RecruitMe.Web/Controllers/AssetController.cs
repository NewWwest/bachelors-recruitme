using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Recruitment.ProfileFiles;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public AssetController()
        {
        }

        [HttpGet]
        [Route("image/{fileid}")]
        public async Task<ActionResult> GetImage(int fileid)
        {
            User user = await AuthenticateUser();

            using (GetFileQueryResult stream = await Get<GetFileQuery>().Execute((user.Id, fileid)))
            using (Image image = Image.FromStream(stream.Data))
            using (MemoryStream m = new MemoryStream())
            {
                image.Save(m, image.RawFormat);
                byte[] imageBytes = m.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);

                return Ok(new
                {
                    file = base64String,
                    contentType = stream.ContentType,
                    contentEncoding = "base64"
                });
            }
        }

        [HttpGet]
        [Route("{fileid}")]
        public async Task<ActionResult> GetFile(int fileid, [FromQuery]int? userId)
        {
            if (userId.HasValue && userId > 0)
            {
                await AuthenticateAdmin();
            }
            else
            {
                User user = await AuthenticateUser();
                userId = user.Id;
            }
            GetFileQueryResult stream = await Get<GetFileQuery>().Execute((userId.Value, fileid));
            return new FileStreamResult(stream.Data, stream.ContentType);
        }
    }
}
