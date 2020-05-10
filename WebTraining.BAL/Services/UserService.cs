using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebTraining.BAL.ApplicationSettings;
using WebTraining.DAL.EntityModels;
using WebTraining.BAL.ExtensionMethods;
using WebTraining.DAL;

namespace WebTraining.BAL.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly TrainingDbContext _dbContext;
        private List<UserRegistration> _users = new List<UserRegistration>();
        public UserService(IOptions<AppSettings> appSettings, TrainingDbContext trainingDb)
        {
            _appSettings = appSettings.Value;
            _dbContext = trainingDb;

        }

        public UserRegistration Authenticate(string username, string password)
        {
            UserRegistration user = _dbContext.UserRegistrations
                .SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null) 
            {
                return null;
            }
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler {
                //TokenLifetimeInMinutes = 5
            };
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public IEnumerable<UserRegistration> GetAll()
        {
            return _users.WithoutPasswords();
        }

        public UserRegistration GetById(int id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            return user.WithoutPassword();
        }

        public bool SaveRegistration(UserRegistration registraionModel)
        {
            _dbContext.Add(registraionModel);
            var result = _dbContext.SaveChanges();
            return true;
        }
    }
}
