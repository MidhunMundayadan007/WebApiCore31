using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebTraining.DAL;
using WebTraining.DAL.EntityModels;

namespace WebTraining.BAL.Services
{
    public class TopicService : ITopicService
    {
        private readonly TrainingDbContext _dbContext;
        public TopicService(TrainingDbContext trainingDb)
        {
            _dbContext = trainingDb;
        }

        public bool SaveTopic(Topics topics)
        {
            topics.CreatedUser = "Admin";
            topics.UpdatedUser = "Admin";
            topics.DateOfCreated = DateTime.Now.ToShortDateString();
            topics.LastUpdated = DateTime.Now.ToShortDateString();
            _dbContext.Add(topics);
            _dbContext.SaveChanges();
            return true;
        }
        public List<Topics> GetTopicByTrainingId(int id)
        {
            return _dbContext.Topics.Where(c => c.TrainingId == id).ToList();
        }
    }
}
