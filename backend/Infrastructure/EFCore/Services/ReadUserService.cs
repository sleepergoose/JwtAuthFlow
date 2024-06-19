using Application.DTO;
using Application.Services;
using Infrastructure.EFCore.Contexts;
using Infrastructure.EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore.Services;

internal class ReadUserService(ReadDbContext dbContext) : IReadUserService
{
    private readonly DbSet<UserReadModel> _users = dbContext.Set<UserReadModel>();

    public async Task<bool> ExistsByEmailAsync(string email)
        => await _users.AnyAsync(u => u.Email == email);
}
