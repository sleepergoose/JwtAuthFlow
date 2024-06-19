using Shared.Exceptions;

namespace Infrastructure.Exceptions;

public sealed class InvalidValueLengthException(string paramName, Type type, int requiredLength)
    : JwtAuthFlowException($"Value length of {paramName} of type {type.Name} must have size {requiredLength}");
