using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTraining.DAL.EntityModels;
using WebTraining.BAL.Services;
using LoggerServcie;
using System.Security.Claims;

namespace WebTraining.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly ILoggerManager _logger;
        public UsersController(IUserService userService, ILoggerManager logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody]UserRegistration registraionModel)
        {
            bool result = _userService.SaveRegistration(registraionModel);
            return Ok(result);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]      
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
                return Forbid();

            var user = _userService.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        [HttpPost]
        public IActionResult Refresh(string token, string refreshToken)
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            return _userService.RefreshToken(token, refreshToken, identity);
        }

   
    }
}
