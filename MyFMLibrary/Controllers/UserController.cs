using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFMLibrary.DTOs;
using MyFMLibrary.Services;

namespace MyFMLibrary.Controllers
{
    /// <summary>
    /// Controller for handling User registration, login and other related actions.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;

        public UserController(ILogger<UserController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
            
        }

        /// <summary>
        /// Registers a new user into the system.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDTO request)
        {
            await _userService.Register(request);
            return Ok();
        }

        /// <summary>
        /// Tries to authenticate the user and returns a JWT token if credentials are valid.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO request)
        {
            var result = await _userService.Login(request);
            return Ok(result);
        }

    }
}
