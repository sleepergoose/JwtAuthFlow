using Shared;

namespace Domain.Exceptions;

internal class EmptyUserIdValueException : JwtAuthFlowException
{
    public EmptyUserIdValueException()
        : base("User ID value cannot be empty")
    {
        
    }
}
