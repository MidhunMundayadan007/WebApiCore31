using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebTraining.DAL.EntityModels;

namespace WebTraining.BAL.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static IEnumerable<UserRegistration> WithoutPasswords(this IEnumerable<UserRegistration> users)
        {
            if (users == null) return null;

            return users.Select(x => x.WithoutPassword());
        }

        public static UserRegistration WithoutPassword(this UserRegistration user)
        {
            if (user == null) return null;

            user.Password = null;
            return user;
        }
    }
}
