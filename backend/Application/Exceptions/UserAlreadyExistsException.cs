using Shared.Exceptions;

namespace Application.Exceptions;

internal class UserAlreadyExistsException : JwtAuthFlowException
{
    public UserAlreadyExistsException(string email)
        : base($"User with email {email} already exists")
    {
    }
}
