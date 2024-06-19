using Domain.Constants;
using Domain.ValueObjects.User;
using Shared.Domain;

namespace Domain.Entities;

public sealed class User : AggregateRoot<UserId>
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
    

    public User(UserId id, UserName name, UserEmail email, UserPasswordHash passwordHash, Role role = Role.User)
    {
        Id = id;
        _name = name;
        _email = email;
        _passwordHash = passwordHash;
        _role = role;
    }

    public void UpdateName(string name)
    {
        this._name = new UserName(name);
    }

    public void UpdatePaswordHash(string passwordHash)
    {
        this._passwordHash = new UserPasswordHash(passwordHash);
    }
}
