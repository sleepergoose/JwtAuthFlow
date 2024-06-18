using Application.DTO;
using Shared.Queries;

namespace Application.Queries;

public sealed record class SearchUserQuery(string searchText) : IQuery<IEnumerable<UserDto>>;

