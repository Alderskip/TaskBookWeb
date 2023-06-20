using static TaskbookServer.Controllers.AuthenticationController;

namespace TaskbookServer.Models
{
    public class AuthenticatedResponse
    {
        public string token { get; set; }
        public string RefreshToken { get; set; }
        public responseUserData user { get; set; }
    }
}
