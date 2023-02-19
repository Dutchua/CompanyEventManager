using CompanyEventManager.Models;
using CompanyEventManager.Querys;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CompanyEventManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BucksController : Controller
    {
        [HttpPost(Name = "BucksInsert")]
        public IActionResult BucksInsert(int attendeeId, decimal amount)
        {
            Bucks bucks= new Bucks(attendeeId, amount, 0);
            return new ObjectResult(BucksQuery.Insert(bucks));
        }

        [HttpGet(Name = "BucksSelectAll")]
        public IActionResult BucksSelectAll()
        {
            return new ObjectResult(BucksQuery.SelectAll());
        }

        [HttpGet("{attendeeId}", Name = "BucksSelectByID")]
        public IActionResult AttendeeSelectId(int attendeeId)
        {
            Bucks bucks = new Bucks(attendeeId);
            Bucks query = BucksQuery.SelectByID(bucks);
            return new ObjectResult(query.attendeeId == 0 ? HttpStatusCode.NotFound : query);
        }

        [HttpDelete("{attendeeId}", Name = "BucksSoftDelete")]
        public IActionResult BucksSoftDelete(int attendeeId)
        {
            Bucks bucks = new Bucks(attendeeId);
            return new ObjectResult(BucksQuery.SoftDelete(bucks));
        }

        //..............NB.....................//
        [HttpDelete(Name = "BucksHardDelete")]
        public IActionResult BucksHardDelete(int personid)
        {
            return new ObjectResult(HttpStatusCode.Forbidden);
        }

        [HttpPatch("{attendeeid}", Name = "BucksUpdate")]
        public IActionResult Update(int attendeeid, decimal amount)
        {
            Bucks bucks = new Bucks(attendeeid, amount);
            return new ObjectResult((BucksQuery.Update(bucks)==1) ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        }
    }
}
