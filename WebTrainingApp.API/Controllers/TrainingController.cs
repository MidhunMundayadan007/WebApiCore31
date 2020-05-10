using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebTraining.BAL.Services;
using WebTraining.DAL.EntityModels;

namespace WebTraining.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private ITrainingService trainingService;

        public TrainingController(ITrainingService trainingservice)
        {
            trainingService = trainingservice;
        }
        [HttpPost("save")]
        public IActionResult SaveTraining([FromBody]Training training)
        {
            bool result = trainingService.SaveTraining(training);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var trainings = trainingService.GetTrainings();
            return Ok(trainings);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var training = trainingService.GetTrainingById(id);
            return Ok(training);
        }
    }
}