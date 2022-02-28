using Microsoft.AspNetCore.Mvc;
using Identity.DTO;
using Identity.Core.Services;

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
                await _userService.AddUser(userDTO);
            }
            catch (Exception ex)
            {

                return NotFound();
            }
            return NoContent();
        }
    }
}
