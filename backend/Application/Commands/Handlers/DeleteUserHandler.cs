using Application.Exceptions;
using Domain.Repositories;
using Shared.Commands;

namespace Application.Commands.Handlers;

public sealed class DeleteUserHandler(IUserRepository repository) : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository _repository = repository;

    public async Task HandlerAsync(DeleteUserCommand command)
    {
        var user = await _repository.GetAsync(command.Id)
            ?? throw new UserNotFoundException(command.Id);

        await _repository.DeleteAsync(user);
    }
}
