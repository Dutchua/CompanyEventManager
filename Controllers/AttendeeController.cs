using CompanyEventManager.Models;
using CompanyEventManager.Querys;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CompanyEventManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttendeeController : Controller
    {
        [HttpPost(Name = "AttendeeInsert")]
        public IActionResult AttendeeInsert(string name, string surname, string accessNumber)
        {
            Attendee attendee = new Attendee(name, surname, accessNumber, 0);
            return new ObjectResult(AttendeeQuery.Insert(attendee));
        }

        [HttpGet(Name = "AttendeeSelectAll")]
        public IActionResult AttendeeSelectAll()
        {
            return new ObjectResult(AttendeeQuery.SelectAll());
        }

        [HttpGet("{attendeeId}", Name = "AttendeeSelectByID")]
        public IActionResult AttendeeSelectId(int attendeeId)
        {
            Attendee attendee = new Attendee(attendeeId);
            Attendee query = AttendeeQuery.SelectByID(attendee);
            return new ObjectResult(query.attendeeId == 0 ? HttpStatusCode.NotFound : query);
        }

        [HttpDelete("{attendeeid}", Name = "AttendeeSoftDelete")]
        public IActionResult AttendeeSoftDelete(int attendeeid)
        {
            Attendee attendee = new Attendee(attendeeid);
            return new ObjectResult(AttendeeQuery.SoftDelete(attendee));
        }

        //..............NB.....................//
        [HttpDelete(Name = "AttendeeHardDelete")]
        public IActionResult AttendeeHardDelete(int personid)
        {
            return new ObjectResult(HttpStatusCode.Forbidden);
        }

        [HttpPatch("{attendeeId}", Name = "AttendeeUpdate")]
        public IActionResult Update(int attendeeId, string name = "", string surname = "", string accessNumber = "")
        {
            Attendee attendee = new Attendee(attendeeId, name, surname, accessNumber, 0);
            return new ObjectResult((AttendeeQuery.Update(attendee) == 1) ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        }
    }
}
