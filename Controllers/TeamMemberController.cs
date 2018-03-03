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
        // GET request to return all team members.
        // No headers needed.
        [HttpGet]
        public Dictionary<string, string> GetAllTeamMembers()
        {
            return Data.Instance.TeamMembers;
        }

        // GET request to return a team member with specific id.
        [HttpGet]
        [Route("/TeamMember/Get")]
        public KeyValuePair<string, string> Get([FromHeader] string id)
        {
            if (Data.Instance.TeamMembers.ContainsKey(id))
                return new KeyValuePair<string, string>(id, Data.Instance.TeamMembers[id]);
            else
                return new KeyValuePair<string, string>();
        }

        // POST request to add a new team member.
        [HttpPost]
        [Route("/TeamMember/Add")]
        public IActionResult Add([FromHeader] string id, [FromHeader] string restrictions)
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

        // PUT request to edit an existing team member.
        [HttpPut]
        [Route("/TeamMember/Edit")]
        public IActionResult Edit([FromHeader] string id, [FromHeader] string restrictions)
        {
            if (Data.Instance.TeamMembers.ContainsKey(id))
            {
                if (restrictions == null) restrictions = "";
                Data.Instance.TeamMembers[id] = restrictions;
                return Ok();
            }
            else
                return BadRequest();
        }

        // DELETE request to delete an existing team member.
        [HttpDelete]
        [Route("/TeamMember/Delete")]
        public IActionResult DeleteTeamMember([FromHeader] string id)
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
