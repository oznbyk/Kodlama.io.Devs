using Application.Features.SocialMediaProfiles.Commands.CreateSocialMediaProfile;
using Application.Features.SocialMediaProfiles.Commands.DeleteSocialMediaProfile;
using Application.Features.SocialMediaProfiles.Commands.UpdateSocialMediaProfile;
using Application.Features.SocialMediaProfiles.Dtos;
using Application.Features.SocialMediaProfiles.Models;
using Application.Features.SocialMediaProfiles.Queries.GetByIdSocialMediaProfile;
using Application.Features.SocialMediaProfiles.Queries.GetListSocialMediaProfile;
using Application.Features.Technologies.Dtos;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaProfileController : BaseController
    {
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateSocialMediaProfileCommand createSocialMediaProfileCommand)
        {
            CreatedSocialMediaProfileDto result = await Mediator.Send(createSocialMediaProfileCommand);
            return Created("", result);

        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] DeleteSocialMediaProfileCommand deleteSocialMediaProfileCommand)
        {
            DeletedSocialMediaProfileDto result = await Mediator.Send(deleteSocialMediaProfileCommand);
            return Ok(result);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListSocialMediaProfileQuery getListSocialMediaProfileQuery = new() { PageRequest = pageRequest };
            SocialMediaProfileListModel result = await Mediator.Send(getListSocialMediaProfileQuery);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] GetByIdSocialMediaProfileQuery getByIdSocialMediaProfileQuery )
        {
            SocialMediaProfileGetByIdDto socialMediaProfileGetByIdDto = await Mediator.Send(getByIdSocialMediaProfileQuery);
            return Ok(socialMediaProfileGetByIdDto);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromQuery] UpdateSocialMediaProfileCommand updateSocialMediaProfileCommand)
        {
            UpdatedSocialMediaProfileDto result = await Mediator.Send(updateSocialMediaProfileCommand);
            return Ok(result);
        }

       
    }
}
