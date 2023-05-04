using System.Security.Claims;

namespace ClassRoom.MVCP.Helper
{
    public class UserHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private ClaimsPrincipal User => _contextAccessor.HttpContext!.User;
        private Guid? _userId;


        public UserHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public Guid UserId => _userId?? Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        
    }
}
