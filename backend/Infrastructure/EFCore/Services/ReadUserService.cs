using Application.DTO;
using Application.Services;
using Infrastructure.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore.Services;

internal class ReadUserService(ReadDbContext dbContext) : IReadUserService
{
    private readonly DbSet<UserDto> _users = dbContext.Set<UserDto>();

    public async Task<bool> ExistsByEmailAsync(string email)
        => await _users.AnyAsync(u => u.Email == email);
}
