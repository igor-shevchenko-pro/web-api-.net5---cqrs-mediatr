using System;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Notes.WebAPI.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediatr;
        protected IMediator Mediator => 
            _mediatr ?? HttpContext.RequestServices.GetService<IMediator>();

        internal Guid UserId =>
            !User.Identity.IsAuthenticated ?
            Guid.Empty :
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}
