﻿namespace Shared.Queries;

public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> ExecuteAsync(TQuery query);
}
