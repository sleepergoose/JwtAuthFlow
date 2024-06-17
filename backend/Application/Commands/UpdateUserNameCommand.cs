using Shared.Commands;

namespace Application.Commands;

public sealed record class UpdateUserNameCommand(
    Guid UserId, string NewName) : ICommand;