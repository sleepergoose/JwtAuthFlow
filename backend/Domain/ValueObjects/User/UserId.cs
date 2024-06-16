using Domain.Exceptions;

namespace Domain.ValueObjects.User;

public sealed record class UserId
{
    public Guid Value { get; }

    public UserId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new EmptyUserIdValueException();
        }

        Value = value;
    }

    public static implicit operator Guid(UserId value)
        => value.Value;

    public static implicit operator UserId(Guid value)
        => new(value);
}
