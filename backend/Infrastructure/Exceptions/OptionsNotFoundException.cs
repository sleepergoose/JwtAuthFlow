using Shared.Exceptions;

namespace Infrastructure.Exceptions;

internal sealed class OptionsNotFoundException : JwtAuthFlowException
{
    public OptionsNotFoundException(string sectionName, Type optionsType) 
        : base($"Could not bind options from the section {sectionName} to type {optionsType.Name}")
    {
    }
}
