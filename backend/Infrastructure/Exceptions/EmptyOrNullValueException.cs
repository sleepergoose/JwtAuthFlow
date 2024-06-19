using Shared.Exceptions;

namespace Infrastructure.Exceptions;

internal sealed class EmptyOrNullValueException(string paramName, Type type)
    : JwtAuthFlowException($"Value of {paramName} of type {nameof(type)} cannot be null or empty")
{
}
