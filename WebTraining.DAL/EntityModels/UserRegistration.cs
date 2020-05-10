using System.Security.Claims;

namespace WebTraining.DAL.EntityModels
{
    public class UserRegistration
    {


        public int Id { get; set; }
        public string Username { get; set; }
        public string EmailId { get; set; }
        public string PasswordHash { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
