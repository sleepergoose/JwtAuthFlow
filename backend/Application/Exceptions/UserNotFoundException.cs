using Shared.Exceptions;

namespace Application.Exceptions;

internal class UserNotFoundException(Guid id) : JwtAuthFlowException($"User with ID {id} is not found")
{
}
