using Shared.Commands;

namespace Application.Commands;

public sealed record class UpdateUserPasswordCommand(
    Guid UserId, string Password) : ICommand;
