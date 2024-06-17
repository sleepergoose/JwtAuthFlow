using Application.Exceptions;
using Domain.Repositories;
using Shared.Commands;

namespace Application.Commands.Handlers;

public sealed class UpdateUserNameHandler(IUserRepository repository) : ICommandHandler<UpdateUserNameCommand>
{
    private readonly IUserRepository _repository = repository;

    public async Task HandlerAsync(UpdateUserNameCommand command)
    {
        var (id, name) = command;

        var user = await _repository.GetAsync(id)
            ?? throw new UserNotFoundException(id);

        user.UpdateName(name);

        await _repository.UpdateAsync(user);
    }
}
