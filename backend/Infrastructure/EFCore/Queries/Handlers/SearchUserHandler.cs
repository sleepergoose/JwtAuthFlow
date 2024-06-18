using Application.DTO;
using Application.Queries;
using Infrastructure.EFCore.Contexts;
using Infrastructure.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Queries;

namespace Infrastructure.EFCore.Queries.Handlers;

internal class SearchUserHandler(ReadDbContext dbContext)
    : IQueryHandler<SearchUserQuery, IEnumerable<UserDto>>
{
    private readonly DbSet<UserReadModel> _users = dbContext.Set<UserReadModel>();

    public async Task<IEnumerable<UserDto>> ExecuteAsync(SearchUserQuery query)
    {
        var dbQuery = _users.AsQueryable();

        if (query is not null && !string.IsNullOrEmpty(query.searchText))
        {
            dbQuery = dbQuery.Where(u => EF.Functions.ILike(u.Name, $"%{query.searchText}%")
                || EF.Functions.ILike(u.Email, $"%{query.searchText}%"));
        }

        return await dbQuery
            .Select(u => u.AsDto())
            .AsNoTracking()
            .ToListAsync();
    }
}
