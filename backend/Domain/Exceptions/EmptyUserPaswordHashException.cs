using Shared;

namespace Domain.Exceptions;

internal class EmptyUserPaswordHashException : JwtAuthFlowException
{
    public EmptyUserPaswordHashException()
        : base("User password hash value cannot be null or empty")
    {
        
    }
}
