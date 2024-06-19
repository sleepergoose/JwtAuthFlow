using Shared.Exceptions;

namespace Infrastructure.Exceptions;

public sealed class EmptyOrNullValueException(string paramName, Type type)
    : JwtAuthFlowException($"Value of {paramName} of type {type.Name} cannot be null or empty")
{
}
