using BrandMonitorTestTask.Infrastructure;
using Dapper;
using MediatR;

namespace BrandMonitorTestTask.DataAccess.Handlers.V1;

using static GetRecord;

public class GetRecordHandler : IRequestHandler<Request, Response>
{
    private const string Query = @$"
        SELECT
            status
        FROM {PgTables.Record}
        WHERE id = @Id";
    
    private readonly IDbContext _dbContext;

    public GetRecordHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        await using var dbConnection = _dbContext.GetConnection();
        await dbConnection.OpenAsync(cancellationToken);
        
        return await dbConnection.QueryFirstAsync<Response>(Query, new {request.Id});
    }
}