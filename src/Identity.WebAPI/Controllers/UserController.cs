using Microsoft.AspNetCore.Mvc;
using Identity.DTO;
using Identity.Core.Services;
using Identity.Infrastructure.WebSocket;
using Identity.Infrastructure.WebSocket.Client;
using Identity.DTO.WebSocketModels;

namespace Identity.WebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> CreateUser([FromBody] SignInUserDTO userDTO)
        {
            try
            {

                HttpContext.Items.Add("ApplicationId", userDTO.ApllicationId);
                await _userService.AddUser(userDTO.ApllicationId, userDTO);
            }
            catch (Exception ex)
            {

                return NotFound();
            }
            return NoContent();
        }
    }
}
