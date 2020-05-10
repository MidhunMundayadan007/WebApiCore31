using System.Collections.Generic;
using WebTraining.DAL.EntityModels;

namespace WebTraining.BAL.Services
{
    public interface ITopicService
    {
        List<Topics> GetTopicByTrainingId(int id);
        bool SaveTopic(Topics topics);
    }
}