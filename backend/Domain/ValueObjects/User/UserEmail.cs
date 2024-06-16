using Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Domain.ValueObjects.User;

public sealed record class UserEmail
{
    public string Value { get; }

    public UserEmail(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new InvalidUserEmailException();
        }

        var email = new EmailAddressAttribute();

        if (!email.IsValid(value))
        {
            throw new InvalidUserEmailFormatException();
        }

        this.Value = value;
    }

    public static implicit operator string(UserEmail value)
        => value.Value;

    public static implicit operator UserEmail(string value)
        => new(value);
}
