namespace Shared.Commands;

public interface ICommandHandler<TCommand> where TCommand : class, ICommand
{
    Task HandlerAsync(TCommand command);
}
