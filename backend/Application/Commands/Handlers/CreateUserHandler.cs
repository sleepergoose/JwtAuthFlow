using Application.Exceptions;
using Application.Services;
using Domain.Factories;
using Domain.Repositories;
using Shared.Commands;

namespace Application.Commands.Handlers;

public sealed class CreateUserHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IReadUserService _readService;
    private readonly IUserRepository _repository;
    private readonly IUserFactory _factory;

    public CreateUserHandler(IUserFactory factory, IUserRepository repository, IReadUserService readService)
    {
        _factory = factory;
        _repository = repository;
        _readService = readService;
    }

    public async Task HandlerAsync(CreateUserCommand command)
    {
        var (id, name, email, passwordHash, role) = command;

        if (await _readService.ExistsByEmailAsync(email))
        {
            throw new UserAlreadyExistsException(email);
        }

        var user = _factory.Create(id, name, email, passwordHash, role);

        await _repository.AddAsync(user);
    }
}
