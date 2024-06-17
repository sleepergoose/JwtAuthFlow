using Domain.Constants;
using Shared.Commands;

namespace Application.Commands;

public sealed record class CreateUserCommand(
    Guid Id,
    string Name,
    string Email,
    string PasswordHash,
    Role Role) : ICommand;

