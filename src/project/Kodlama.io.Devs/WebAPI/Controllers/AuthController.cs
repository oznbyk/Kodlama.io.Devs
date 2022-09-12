using Application.Features.Users.Commands.Register;
using Application.Features.Users.Dtos;
using Application.Features.Users.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand registerUserCommand)
        {
            RegisterResponseDto result = await Mediator.Send(registerUserCommand);
            return Created("", result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserQuery loginUserQuery)
        {
            LoginResponseDto result = await Mediator.Send(loginUserQuery);
            return Ok(result);
        }


    }
}
