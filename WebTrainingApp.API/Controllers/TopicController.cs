using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTraining.BAL.Services;
using WebTraining.DAL.EntityModels;

namespace WebTraining.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private ITopicService topicService;

        public TopicController(ITopicService topicService)
        {
            this.topicService = topicService;
        }
        [HttpPost("save")]
        public IActionResult SaveTopic([FromBody]Topics topic)
        {
            bool result = topicService.SaveTopic(topic);
            return Ok(result);
        }
    }
}