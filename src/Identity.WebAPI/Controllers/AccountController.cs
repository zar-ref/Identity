using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity; 
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Identity.Core.Services;
using Identity.DTO;
using Identity.Entities.Entities.Identity;
using Identity.Core.Services.Interfaces;

namespace Identity.WebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
 
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> CreateUser([FromBody] SignInUserDTO userDTO)
        {
 
            try
            {
                HttpContext.Items.Add("ApplicationId", userDTO.ApllicationId);
                await _accountService.AddAccount(userDTO.ApllicationId, userDTO);
            }
            catch (Exception ex)
            {

                return NotFound();
            }
            return NoContent();
        }
    }
}
