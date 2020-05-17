using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using WebTraining.DAL.EntityModels;

namespace WebTraining.BAL.Services
{
    public interface IUserService
    {
        UserRegistrationViewModel Authenticate(string username, string password);
        IEnumerable<UserRegistrationViewModel> GetAll();
        UserRegistrationViewModel GetById(int id);
        bool SaveRegistration(UserRegistration registraionModel);
        IActionResult RefreshToken(string token, string refreshToken, System.Security.Claims.ClaimsIdentity identity);
    }
}
