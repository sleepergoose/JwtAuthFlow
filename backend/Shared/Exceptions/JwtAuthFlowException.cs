namespace Shared.Exceptions;

public abstract class JwtAuthFlowException : Exception
{
    protected JwtAuthFlowException(string message) : base(message)
    {

    }
}
