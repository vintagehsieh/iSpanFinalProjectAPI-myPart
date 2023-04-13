using api.iSMusic.Models.DTOs.ActivityDTOs;
using api.iSMusic.Models.Services.Interfaces;

namespace api.iSMusic.Models.Services
{
	public class MemberService
	{
		private readonly IMemberRepository _memberRepository;

		
		private readonly IActivityRepository _activityRepository;


        public MemberService(IMemberRepository repo,  IActivityRepository activityRepository)
		{
			_memberRepository = repo;
			_activityRepository = activityRepository;

        }

		
        public (bool Success, string Message, IEnumerable<ActivityIndexDTO>Dtos) GetMemberFollowedActivities(int memberId)
		{
			if (CheckMemberExistence(memberId) == false) return (false, "會員不存在", new List<ActivityIndexDTO>());

			var dtos = _activityRepository.GetMemberFollowedActivities(memberId);
			return (true, string.Empty, dtos);
        }


        public (bool Success, string Message) FollowActivity(int memberId, int activityId, DateTime attendDate)
		{
			if (CheckMemberExistence(memberId) == false) return (false, "會員不存在");
			if(CheckActivityExistence(activityId) == false) return (false, "活動不存在");

            _activityRepository.FollowActivity(memberId, activityId, attendDate);
            return (true, "成功新增");
        }
 

        public (bool Success, string Message) UnfollowActivity(int memberId, int activityId)
        {
            if (CheckMemberExistence(memberId) == false) return (false, "會員不存在");

            if (CheckActivityExistence(activityId) == false) return (false, "活動不存在");

            _activityRepository.UnfollowActivity(memberId, activityId);
            return (true, "成功刪除");
        }

      

		private bool CheckMemberExistence(int memberId)
		{
			var member = _memberRepository.GetMemberById(memberId);

			return member != null;
		}

        private bool CheckActivityExistence(int activityId)
        {
            var activity = _activityRepository.GetActivityByIdForCheck(activityId);

            return activity != null;
        }

    }
}
