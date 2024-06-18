using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.User;
using Infrastructure.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore.Repositories;

internal class UserRepository(WriteDbContext dbContext) : IUserRepository
{
    private readonly WriteDbContext _dbContext = dbContext;
    private readonly DbSet<User> _users = dbContext.Set<User>();

    public Task<User?> GetAsync(UserId id)
        => _users.SingleOrDefaultAsync(u => u.Id == id);

    public async Task AddAsync(User user)
    {
        _users.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}
