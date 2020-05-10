using System;
using System.Collections.Generic;
using System.Text;

namespace WebTraining.DAL.EntityModels
{
    public class Training
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string DateOfCreated { get; set; }
        public string LastUpdated { get; set; }
        public string ImageUrl { get; set; }
        public string CourseDescription { get; set; }
    }
    public class Topics
    {
        public int Id { get; set; }
        public int TrainingId { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string CreatedUser { get; set; }
        public string UpdatedUser { get; set; }
        public int NumberOfVotes { get; set; }
        public string Comments { get; set; }
        public string DateOfCreated { get; set; }
        public string LastUpdated { get; set; }
        public string Level { get; set; }
    }
}