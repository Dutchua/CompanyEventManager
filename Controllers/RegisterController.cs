using CompanyEventManager.Models;
using CompanyEventManager.Querys;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CompanyEventManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : Controller
    {
        [HttpPost(Name = "RegisterInsert")]
        public IActionResult RegisterInsert(int attendeeId, int registered)
        {
            Register register = new Register(attendeeId, registered, 0);
            return new ObjectResult((RegisterQuery.Insert(register) == 1) ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        }

        [HttpGet(Name = "RegisterSelectAll")]
        public IActionResult RegisterSelectAll()
        {
            return new ObjectResult(RegisterQuery.SelectAll());
        }

        [HttpGet("{attendeeId}", Name = "RegisterSelectByID")]
        public IActionResult RegisterSelectId(int attendeeId)
        {
            Register register = new Register(attendeeId);
            Register query = RegisterQuery.SelectByID(register);
            return new ObjectResult(query.attendeeId == 0 ? HttpStatusCode.NotFound : query);
        }

        [HttpDelete("{attendeeId}", Name = "RegisterSoftDelete")]
        public IActionResult BucksSoftDelete(int attendeeId)
        {
            Register register = new Register(attendeeId);
            return new ObjectResult(RegisterQuery.SoftDelete(register));
        }

        //..............NB.....................//
        [HttpDelete(Name = "RegisterHardDelete")]
        public IActionResult RegisterHardDelete(int personid)
        {
            return new ObjectResult(HttpStatusCode.Forbidden);
        }

        [HttpPatch("{attendeeid}", Name = "RegisterUpdate")]
        public IActionResult RegisterUpdate(int attendeeid, int registered)
        {
            Register register = new Register(attendeeid, registered);
            return new ObjectResult((RegisterQuery.Update(register) == 1) ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        }
    }
}
