using Domain.Constants;
using Domain.ValueObjects.User;
using Shared.Domain;

namespace Domain.Entities;

public sealed  class User : AggregateRoot<UserId>
{
    private UserName _name;
    private UserEmail _email;
    private UserPasswordHash _passwordHash;
    private Role _role;

    #pragma warning disable CS8618
    private User()
    #pragma warning restore CS8618
    {

    }
    

    public User(UserName name, UserEmail email, UserPasswordHash passwordHash, Role role = Role.User)
    {
        _name = name;
        _email = email;
        _passwordHash = passwordHash;
        _role = role;
    }
}
