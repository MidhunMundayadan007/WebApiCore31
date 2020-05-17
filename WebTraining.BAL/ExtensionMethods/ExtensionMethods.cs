using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebTraining.DAL.EntityModels;

namespace WebTraining.BAL.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static IEnumerable<UserRegistrationViewModel> WithoutPasswords(this IEnumerable<UserRegistrationViewModel> users)
        {
            if (users == null) return null;

            return users.Select(x => x.WithoutPassword());
        }

        public static UserRegistrationViewModel WithoutPassword(this UserRegistrationViewModel user)
        {
            if (user == null) return null;

            user.User.PasswordHash = null;
            return user;
        }
    }
}
