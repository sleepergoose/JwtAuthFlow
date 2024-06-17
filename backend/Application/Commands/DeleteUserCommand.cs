using Shared.Commands;

namespace Application.Commands;

public sealed record class DeleteUserCommand(Guid Id) : ICommand;
