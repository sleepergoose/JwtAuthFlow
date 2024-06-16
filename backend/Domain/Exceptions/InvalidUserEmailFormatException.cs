using Shared;

namespace Domain.Exceptions;

internal class InvalidUserEmailFormatException : JwtAuthFlowException
{
    public InvalidUserEmailFormatException()
        : base("User email has invalid format")
    {
        
    }
}
