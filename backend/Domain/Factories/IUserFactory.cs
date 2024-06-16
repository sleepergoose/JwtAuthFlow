using Domain.Constants;
using Domain.Entities;
using Domain.ValueObjects.User;

namespace Domain.Factories;

public interface IUserFactory
{
    User Create(UserId id, UserName name, UserEmail email, UserPasswordHash passwordHash,Role role);
}
