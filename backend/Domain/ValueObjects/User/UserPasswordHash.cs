using Domain.Exceptions;
using Shared.Domain;

namespace Domain.ValueObjects.User;

public sealed record class UserPasswordHash
{
    public string Value { get; }

    public UserPasswordHash(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyUserPaswordHashException();
        }

        Value = value;
    }

    public static implicit operator string(UserPasswordHash value)
        => value.Value;

    public static implicit operator UserPasswordHash(string value)
    => new(value);
}
