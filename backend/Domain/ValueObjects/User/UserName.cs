using Domain.Exceptions;

namespace Domain.ValueObjects.User;
public sealed record class UserName
{
    public string Value { get; }

    public UserName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyUserNameException();
        }

        if (value.Length < 3 || value.Length > 64)
        {
            throw new InvalidUserNameLengthException();
        }

        Value = value;
    }

    public static implicit operator string (UserName value)
        => value.Value;

    public static implicit operator UserName (string value)
        => new (value);
}
