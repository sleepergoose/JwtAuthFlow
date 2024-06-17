using Application.Exceptions;
using Domain.Repositories;
using Shared.Commands;

namespace Application.Commands.Handlers;

public sealed class UpdateUserPasswordHandler(IUserRepository userRepository) : ICommandHandler<UpdateUserPasswordCommand>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task HandlerAsync(UpdateUserPasswordCommand command)
    {
        var (id, passwordHash) = command;

        var user = await _userRepository.GetAsync(id)
            ?? throw new UserNotFoundException(id);

        user.UpdatePaswordHash(passwordHash);

        await _userRepository.UpdateAsync(user);
    }
}
