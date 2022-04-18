using Identity.Core.Services.Interfaces;

namespace Identity.WebAPI.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAccountService _accountService;

        public JwtMiddleware(RequestDelegate next, IAccountService accountService)
        {
            _next = next;
            _accountService = accountService;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var accountCredentials = _accountService.ValidateJwtToken(token);
            if (accountCredentials != null)
            {
                context.Items.Add("User", accountCredentials.Email);
                context.Items.Add("ApplicationId", accountCredentials.ApplicationId);
                context.Items.Add("Roles", accountCredentials.Roles);
            }
            await _next(context);

        }
    }
}
