using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.DeleteTechnology;
using Application.Features.Technologies.Commands.UpdateTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using Application.Features.Technologies.Queries;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologiesController : BaseController
    {
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateTechnologyCommand createTechnologyCommand)
        {
         CreatedTechnologyDto result   = await Mediator.Send(createTechnologyCommand);
            return Created("", result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] DeleteTechnologyCommand deleteTechnologyCommand)
        {
            DeletedTechnologyDto result = await Mediator.Send(deleteTechnologyCommand);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromQuery] UpdateTechnologyCommand updateTechnologyCommand)
        {
            UpdatedTechnologyDto result = await Mediator.Send(updateTechnologyCommand);
            return Ok(result);
        }




        [HttpGet("GetList")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTechnologyQuery getListTechnologyQuery = new GetListTechnologyQuery { PageRequest = pageRequest };
            TechnologyListModel result = await Mediator.Send(getListTechnologyQuery);
            return Ok(result);
        }



    }

  
}
