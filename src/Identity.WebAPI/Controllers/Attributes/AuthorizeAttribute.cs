using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.WebAPI.Controllers.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string [] AllowedRoles { get ; set; } //Should be setted in every controller enpoint
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var roles = (string[])context.HttpContext.Items["Roles"];

            bool rolesMatch = true;
            foreach (var role in roles)
            {
                if (!AllowedRoles.Any(_allowedRole => _allowedRole == role))
                {
                    rolesMatch = false;
                    break;

                }
            }
            if(!rolesMatch)
                context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}
