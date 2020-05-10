using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebTraining.DAL;
using WebTraining.DAL.EntityModels;

namespace WebTraining.BAL.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly TrainingDbContext _dbContext;
        public TrainingService(TrainingDbContext trainingDb)
        {
            _dbContext = trainingDb;
        }

        public bool SaveTraining(Training training)
        {
            training.DateOfCreated = DateTime.Now.ToShortDateString();
            training.LastUpdated = DateTime.Now.ToShortDateString();
            _dbContext.Add(training);
            _dbContext.SaveChanges();
            return true;
        }

        public Training GetTrainingById(int id)
        {
            return _dbContext.Trainings.Where(c => c.Id == id).FirstOrDefault();
        }

        public List<Training> GetTrainings()
        {
            return _dbContext.Trainings.ToList();
        }
    }
}

