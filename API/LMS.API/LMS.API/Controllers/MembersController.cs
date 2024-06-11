using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using LMS.API.Business_Layer;

namespace LMS.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberDataExtractor _memberDataExtractor;

        public MembersController(IMemberDataExtractor memberDataExtractor)
        {
            _memberDataExtractor= memberDataExtractor;
        }

        [HttpGet("GetAllMembers")]
        public ActionResult<List<Member>> GetAllMembers()
        {
            try
            {
                List<Member> members = _memberDataExtractor.GetAllMembers();
                return Ok(members);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500,"Internal Server Error");
            }
            
            
        }

        [HttpGet("GetMemberById")]
        public ActionResult<Member> GetMemberById(int memberId)
        {
            try
            {
                Member member = _memberDataExtractor.GetMemberById(memberId);

                if (member == null)
                    return NotFound(); // 404 Not Found

                return Ok(member);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("AddMember")]
        public IActionResult AddMember([FromBody] Member member)
        {
            try
            {
                return new JsonResult(_memberDataExtractor.AddMember(member));
            }
            catch (Exception ex)
            {
                // Log the exception
                return new JsonResult("Internal Server Error");
            }
        }
        [HttpPost("UpdateMember")]
        public IActionResult UpdateMember([FromBody] Member member)
        {
            try
            {
                return new JsonResult(_memberDataExtractor.UpdateMember(member.MemberId,member));
            }
            catch (Exception ex)
            {
                // Log the exception
                return new JsonResult("Internal Server Error");
            }
        }
        [HttpDelete("DeleteMember")]
        public IActionResult DeleteMember(int memberId)
        {
            try
            {
                return new JsonResult(_memberDataExtractor.DeleteMember(memberId));
            }
            catch (Exception ex)
            {
                // Log the exception
                return new JsonResult("Internal Server Error");
            }
        }

        // Add other methods for update and delete as needed
    }

}
