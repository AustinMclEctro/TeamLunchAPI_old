using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TeamLunchAPI.Controllers
{
    [Produces("application/json")]
    [Route("/TeamMember")]
    public class TeamMemberController : Controller
    {
        // GET: /TeamMember
        // Does not use any headers.
        [HttpGet]
        public Dictionary<int, string> GetAllTeamMembers()
        {
            //return new string[] { "default", "get" };
            return Data.Instance.TeamMembers;
        }

        // Uses headers to retrieve individual team member.
        [HttpGet]
        [Route("/TeamMember/Get")]
        public KeyValuePair<int, string> Get([FromHeader] int id)
        {
            if (Data.Instance.TeamMembers.ContainsKey(id))
                return new KeyValuePair<int, string>(id, Data.Instance.TeamMembers[id]);
            else
                return new KeyValuePair<int, string>();
        }

        [HttpPost]
        [Route("/TeamMember/Add")]
        public IActionResult Add([FromHeader] int id, [FromHeader] string restrictions)
        {
            if (!Data.Instance.TeamMembers.ContainsKey(id))
            {
                if (restrictions == null) restrictions = "";
                Data.Instance.TeamMembers.Add(id, restrictions);
                return Ok();
            }
            else
                return BadRequest();
        }

        //// GET: /TeamMember/5
        //[HttpGet("{id}", Name = "Get")]
        //public string[] Get(int id)
        //{
        //    return new string[] { "value" };
        //}

        //// POST: /TeamMember
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: /TeamMember/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE: /TeamMember/Delete
        [HttpDelete]
        [Route("/TeamMember/Delete")]
        public IActionResult DeleteTeamMember([FromHeader] int id)
        {
            if (Data.Instance.TeamMembers.ContainsKey(id))
            {
                Data.Instance.TeamMembers.Remove(id);
                return Ok();
            }
            else
                return BadRequest("Could not remove");
        }
    }
}
