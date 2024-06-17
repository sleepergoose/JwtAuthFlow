using Application.DTO;
using Shared.Queries;

namespace Application.Queries;

public sealed record class GetUserQuery(Guid id) : IQuery<UserDto>;
