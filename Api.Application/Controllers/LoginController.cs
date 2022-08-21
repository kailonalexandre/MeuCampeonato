using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<Object> Login([FromBody] LoginDto loginDtoEntity, [FromServices] ILoginService service)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (loginDtoEntity == null)
                return BadRequest();

            try
            {
                var result = await service.FindByLogin(loginDtoEntity);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
