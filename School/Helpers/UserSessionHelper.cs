namespace School.Helpers
{
    public class UserSessionHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Method to check if the user is logged in by verifying the presence of the token in the session
        public bool IsUserLoggedIn()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken");
            return !string.IsNullOrEmpty(token);
        }
    }
}
