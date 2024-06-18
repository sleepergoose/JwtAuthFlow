using Application.DTO;
using Infrastructure.EFCore.Models;

namespace Infrastructure.EFCore.Queries;

internal static class Extensions
{
    public static UserDto AsDto(this UserReadModel user)
        => new ()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
        };
}
