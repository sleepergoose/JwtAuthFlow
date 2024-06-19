using Shared.Exceptions;

namespace Infrastructure.Exceptions;

internal sealed class InvalidValueLengthException(string paramName, Type type, int requiredLength)
    : JwtAuthFlowException($"Value length of {paramName} of type {nameof(type)} must have size {requiredLength}");
