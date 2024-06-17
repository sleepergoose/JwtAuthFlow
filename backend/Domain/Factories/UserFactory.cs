using Domain.Constants;
using Domain.Entities;
using Domain.ValueObjects.User;

namespace Domain.Factories;

internal class UserFactory : IUserFactory
{
    public User Create(UserId id, UserName name, UserEmail email, UserPasswordHash passwordHash, Role role)
        => new User(name, email, passwordHash, role);
}
