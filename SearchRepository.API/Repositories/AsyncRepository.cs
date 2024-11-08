using System.Data;
using System.Xml.Serialization;
using SqlKata;
using SqlKata.Execution;

namespace SearchRepository.API.Repositories;

public class AsyncRepository :IAsyncRepository
{

    private QueryFactory _queryFactory;

    public AsyncRepository(SearchRepositoryContext db)
    {
        _queryFactory = db.SqlQueryFactory;
    }


    public void SetConnection(SearchRepositoryContext db)
    {
        _queryFactory = db.SqlQueryFactory;
    }

    public Query GetQueryBuilder()
    {
        return new Query();
    }

    public QueryFactory GetQueryFactory() => _queryFactory;

    public Query GetQueryBuilder(string tableName)
    {
        return _queryFactory.Query(tableName);
    }

    public Query GetIncludeBuilder(string tableName)
    {
        return _queryFactory.Query(tableName);
    }

    public Query GetIncludeBuilder(Query query)
    {
        return _queryFactory.Query().From(query);
    }

    public async Task<int> ExecuteAsync(Query query, CancellationToken cancellationToken = default)
    {
        return  await _queryFactory.ExecuteAsync(query, cancellationToken: cancellationToken);
    }
    public async Task<bool> ExistsAsync(Query query, CancellationToken cancellationToken = default)
        => await ExistsAsync(query, null, cancellationToken);

    public async Task<bool> ExistsAsync(Query query, IDbTransaction? transaction = null,
        CancellationToken cancellationToken = default)
    {
        var finalQuery = CloneToQueryFactory(query);
        return await finalQuery.ExistsAsync(transaction,cancellationToken: cancellationToken);
    }


    public async Task<T?> GetAsync<T>(Query query , IDbTransaction? transaction = null,
        CancellationToken ct = default)
    {
        var finalQuery = CloneToQueryFactory(query);
        return await finalQuery.FirstOrDefaultAsync<T>(transaction,cancellationToken: ct);
    }


    private Query CloneToQueryFactory(Query sourceQuery)
    {
        if(sourceQuery is XQuery)
        {
            return sourceQuery;
        }

        var resultQuery = _queryFactory.Query().From(sourceQuery);

        resultQuery.Includes  = sourceQuery.Includes;
        resultQuery.Method = sourceQuery.Method;
    
        return resultQuery;
    }

}