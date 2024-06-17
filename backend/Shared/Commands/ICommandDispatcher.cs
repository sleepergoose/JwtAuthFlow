namespace Shared.Commands;

public interface ICommandDispatcher
{
    Task DispatchCommand<TCommand>(TCommand command) where TCommand : class, ICommand;
}
