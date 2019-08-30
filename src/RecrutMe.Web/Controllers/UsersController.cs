using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecrutMe.Logic.Data;

namespace RecrutMe.Web.Controllers
{
    [Route("api/users")]
    public class UsersController : BaseController
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UsersController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        [Route("xd")]
        public JsonResult xd()
        {
            _applicationDbContext.ExampleClasses.Add(new Logic.Data.Entities.ExampleClass() { Value = "XD" });
            _applicationDbContext.SaveChanges();
            return new JsonResult(_applicationDbContext.ExampleClasses.ToList());
        }
    }
}
