using Application.DTO;
using Shared.Queries;

namespace Application.Queries;

public sealed record class SearchUser(string searchText) : IQuery<IEnumerable<UserDto>>;

