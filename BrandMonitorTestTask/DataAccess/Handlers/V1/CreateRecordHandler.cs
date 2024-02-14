using BrandMonitorTestTask.Infrastructure;
using Dapper;
using MediatR;

namespace BrandMonitorTestTask.DataAccess.Handlers.V1;

using static CreateRecord;

public class CreateRecordHandler : IRequestHandler<Request, Response>
{
    private static readonly string Query = @$"
        INSERT INTO {PgTables.Record} (status, updated_at) VALUES ('{RecordStatus.Created}'::record_status, @DateNow)
        RETURNING id";

    private readonly IDbContext _dbContext;

    public CreateRecordHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        await using var dbConnection = _dbContext.GetConnection();
        await dbConnection.OpenAsync(cancellationToken);
        
        return await dbConnection.QuerySingleAsync<Response>(Query, request.AsParameters);
    }
}