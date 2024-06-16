using Shared.Exceptions;

namespace Domain.Exceptions;

internal class InvalidUserEmailException : JwtAuthFlowException
{
    public InvalidUserEmailException()
        : base("User email value cannot be null or empty")
    {
        
    }
}
