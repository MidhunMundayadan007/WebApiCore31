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
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Http;

namespace WebTraining.BAL.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly TrainingDbContext _dbContext;
        private List<UserRegistrationViewModel> _users = new List<UserRegistrationViewModel>();
        public UserService(IOptions<AppSettings> appSettings, TrainingDbContext trainingDb)
        {
            _appSettings = appSettings.Value;
            _dbContext = trainingDb;

        }

        public UserService()
        {
        }

        public UserRegistrationViewModel Authenticate(string username, string password)
        {
            UserRegistration userregDetails = _dbContext.UserRegistrations
                .SingleOrDefault(x => x.Username == username && x.PasswordHash == password);


            UserRegistrationViewModel user = new UserRegistrationViewModel
            {
                User = userregDetails , Token ="", RefreshToken=""
            };

            // return null if user not found
            if (user == null) 
            {
                return null;
            }
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler {
                //TokenLifetimeInMinutes] = 5
            };
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.User.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature),
                
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.RefreshToken = GenerateRefreshToken();
            bool SaveRefreshtoken = SaveRefreshtokenDetails(user);
            return user.WithoutPassword();
        }

        private bool SaveRefreshtokenDetails(UserRegistrationViewModel user)
        {
            UserLoggedInDetails userLoggedInDetails = new UserLoggedInDetails()
            {
                UserId = user.User.Id,
                LoggedInDateandTime = DateTime.Now.ToString(),
                RefreshToken = user.RefreshToken,
                Token = user.Token,
                
            };

            _dbContext.Add(userLoggedInDetails);
            var result = _dbContext.SaveChanges();
            return true;
        }

        public IEnumerable<UserRegistrationViewModel> GetAll()
        {
            return _users.WithoutPasswords();
        }

        public UserRegistrationViewModel GetById(int id)
        {
            var user = _users.FirstOrDefault(x => x.User.Id == id);
            return user.WithoutPassword();
        }

        public IActionResult RefreshToken(string token, string refreshToken, ClaimsIdentity identity)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            var username = principal.Identity.Name;
            var savedRefreshToken = GetRefreshToken(username,token); 
            //retrieve the refresh token from a data store
            if (savedRefreshToken != refreshToken)
                throw new SecurityTokenException("Invalid refresh token");

            var newJwtToken = GenerateToken(principal.Claims);
            var newRefreshToken = GenerateRefreshToken();
            //DeleteRefreshToken(username, refreshToken);
            //SaveRefreshToken(username, newRefreshToken);

            return new ObjectResult(new
            {
                token = newJwtToken,
                refreshToken = newRefreshToken
            });
        }

        private string GetRefreshToken(string userName,string token)
        {
            // Cast to ClaimsIdentity.
            

            var refreshToken = _dbContext.UserRegistrations.Join(_dbContext.
                UserLoggedInDetails, ur => ur.Id, ul => ul.UserId, (ur, ul)
                   => new
                   {
                   });

            return refreshToken.First().ToString();
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }


        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("the server key used to sign the JWT token is here, use more than 16 chars"));

            var jwt = new JwtSecurityToken(issuer: "Blinkingcaret",
                audience: "Everyone",
                claims: claims, //the user's claims, for example new Claim[] { new Claim(ClaimTypes.Name, "The username"), //... 
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt); //the method is called WriteToken but returns a string
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("the server key used to sign the JWT token is here, use more than 16 chars")),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        public bool SaveRegistration(UserRegistration registraionModel)
        {
            _dbContext.Add(registraionModel);
            var result = _dbContext.SaveChanges();
            return true;
        }
    }
}
