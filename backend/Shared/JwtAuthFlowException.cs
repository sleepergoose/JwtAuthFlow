namespace Shared;

public abstract class JwtAuthFlowException : Exception
{
    protected JwtAuthFlowException(string message) : base(message)
    {
        
    }
}
