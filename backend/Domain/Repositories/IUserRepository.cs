using Domain.Entities;
using Domain.ValueObjects.User;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetAsync(UserId id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}
