using System;

namespace Notes.Application.Interfaces.Services.User
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
    }
}
