using Infrastructure.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Options;

internal sealed record class ArgonOptions
{
    private readonly int _byteSize = 32;
    private string _knownSecret = string.Empty;

    public string KnownSecret
    {
        get
        {
            return _knownSecret;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new EmptyOrNullValueException(nameof(KnownSecret), value.GetType());
            }

            if (value.Length != _byteSize)
            {
                throw new InvalidValueLengthException(nameof(KnownSecret), value.GetType(), _byteSize);
            }

            _knownSecret = value;
        }
    }

    public byte[] GetBytes()
        => Convert.FromBase64String(KnownSecret);
}
