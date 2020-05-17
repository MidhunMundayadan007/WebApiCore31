using System.Security.Claims;

namespace WebTraining.DAL.EntityModels
{
    public class UserRegistration
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string EmailId { get; set; }
        public string PasswordHash { get; set; }

    }

    public class UserRegistrationViewModel
    {
        public UserRegistration User { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

    }
    public class UserRoleTable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RoleName { get; set; }
    }
    public class UserLoggedInDetails
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string LoggedInDateandTime { get; set; }
    }


}
