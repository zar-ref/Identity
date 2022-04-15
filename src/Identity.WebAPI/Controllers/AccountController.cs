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

namespace Identity.WebAPI.Controllers
{ 
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> Login([FromBody] SignInUserDTO userDTO)
        {

            try
            {
                HttpContext.Items.Add("ApplicationId", userDTO.ApllicationId);
                var token = await _accountService.Login(userDTO.ApllicationId, userDTO);
                var x = token.ValidFrom;
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            catch (Exception ex)
            {

                return NotFound();
            }
            return NoContent();
        }
         
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

        [Authorize(Roles = "Admin,Manager")]
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
