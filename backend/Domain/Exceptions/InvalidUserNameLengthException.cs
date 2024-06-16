using Shared.Exceptions;

namespace Domain.Exceptions;

internal class InvalidUserNameLengthException : JwtAuthFlowException
{
    public InvalidUserNameLengthException()
        : base("User name must be 3 to 64 characters long")
    {
        
    }
}
