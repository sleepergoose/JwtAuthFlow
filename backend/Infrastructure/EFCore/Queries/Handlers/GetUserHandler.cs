using Application.DTO;
using Application.Queries;
using Infrastructure.EFCore.Contexts;
using Infrastructure.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Queries;

namespace Infrastructure.EFCore.Queries.Handlers;

internal class GetUserHandler(ReadDbContext dbContext) : IQueryHandler<GetUserQuery, UserDto>
{
    private readonly DbSet<UserReadModel> _users = dbContext.Set<UserReadModel>();

    public Task<UserDto> ExecuteAsync(GetUserQuery query)
        => _users
            .Where(u => u.Id == query.id)
            .Select(u => u.AsDto())
            .AsNoTracking()
            .SingleOrDefaultAsync()!;
}
    