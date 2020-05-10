using System.Collections.Generic;
using WebTraining.DAL.EntityModels;

namespace WebTraining.BAL.Services
{
    public interface ITrainingService
    {
        Training GetTrainingById(int id);
        List<Training> GetTrainings();
        bool SaveTraining(Training training);
    }
}