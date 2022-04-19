using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Identity.Core.Services;
using Identity.DTO;
using Identity.Entities.Entities.Identity;
using Identity.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Identity.WebAPI.Controllers.Attributes;
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

        [Attributes.AllowAnonymous]
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

        [Attributes.AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> Login([FromBody] SignInUserDTO userDTO)
        {

            try
            {
                HttpContext.Items.Add("ApplicationId", userDTO.ApllicationId);
                var token = await _accountService.Login(userDTO.ApllicationId, userDTO);
                return Ok(new
                {
                    token = token
                });
            }
            catch (Exception ex)
            {

                return NotFound();
            }
            return NoContent();
        }

        [Attributes.Authorize]
        [HttpPost("[action]")]

        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> ChangePasseord([FromBody] ChangePasswordDTO changePasswordDto)
        {

            try
            {
                await _accountService.ChangePassowrd(changePasswordDto);
            }
            catch (Exception ex)
            {

                return NotFound();
            }
            return NoContent();
        }

        [Attributes.Authorize(AllowedRoles = new string[] {"Admin"})]
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> ChangePasseordAdmin([FromBody] ChangePasswordDTO changePasswordDto)
        {

            try
            {
                await _accountService.ChangePassowrd(changePasswordDto);
            }
            catch (Exception ex)
            {

                return NotFound();
            }
            return NoContent();
        }


    }
}
