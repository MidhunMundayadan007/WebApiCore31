using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebTraining.DAL.EntityModels;

namespace WebTraining.DAL
{
    public class TrainingDbContext : DbContext
    {
        public TrainingDbContext(DbContextOptions<TrainingDbContext> option)
            : base(option)
        {

        }
        public DbSet<UserRoleTable> UserRoles { get; set; }
        public DbSet<UserRegistration> UserRegistrations { get; set; }
        public DbSet<UserLoggedInDetails> UserLoggedInDetails { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Topics> Topics { get; set; }
    }
}
