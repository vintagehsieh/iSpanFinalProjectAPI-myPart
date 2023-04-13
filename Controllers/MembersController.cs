using api.iSMusic.Models.Infrastructures.Extensions;
using api.iSMusic.Models.Services;
using api.iSMusic.Models.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.iSMusic.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class MembersController : ControllerBase
	{
		private readonly IMemberRepository _memberRepository;

		
		private readonly MemberService _memberService;


        public MembersController(IMemberRepository memberRepo,IActivityRepository activityRepository)
		{
			_memberRepository = memberRepo;
		
            _memberService = new(_memberRepository, activityRepository);			
        }


		[HttpGet]
		[Route("Activities")]
		public IActionResult GetMemberFollowedActivities()
		{
            int memberId = this.GetMemberId();
            var result = _memberService.GetMemberFollowedActivities(memberId);
			if (!result.Success)
			{
				return BadRequest(result.Message);
			}

			return Ok(result.Dtos.Select(dto => dto.ToIndexVM()));

        }

		[HttpPost]
		[Route("Activities/{activityId}/{attendDate}")]
		public IActionResult FollowActivity([FromRoute] int activityId, [FromRoute] DateTime attendDate)
		{
			
			int memberId = this.GetMemberId();
			//DateTime attendDateTime = DateTime.ParseExact(attendDate, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
			var result = _memberService.FollowActivity(memberId, activityId, attendDate);

			if (!result.Success)
			{
				return BadRequest(result.Message);
			}

			return Ok(result.Message);
		}

		[HttpDelete]
		[Route("Activities/{activityId}")]
		public IActionResult UnfollowActivity(int activityId)
		{
			int memberId = this.GetMemberId();
			var result = _memberService.UnfollowActivity(memberId, activityId);

			if (!result.Success)
			{
				return BadRequest(result.Message);
			}

			return Ok(result.Message);
		}
	}
}
