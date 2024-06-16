using Shared;

namespace Domain.Exceptions;

internal class EmptyUserNameException : JwtAuthFlowException
{
    public EmptyUserNameException()
        : base("User Name value cannot be null or empty")
    {
        
    }
}
