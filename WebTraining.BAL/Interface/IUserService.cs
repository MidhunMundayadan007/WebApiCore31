using System;
using System.Collections.Generic;
using System.Text;
using WebTraining.DAL.EntityModels;

namespace WebTraining.BAL.Services
{
    public interface IUserService
    {
        UserRegistration Authenticate(string username, string password);
        IEnumerable<UserRegistration> GetAll();
        UserRegistration GetById(int id);
        bool SaveRegistration(UserRegistration registraionModel);
    }
}
