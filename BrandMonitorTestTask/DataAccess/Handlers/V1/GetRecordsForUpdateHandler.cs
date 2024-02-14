using BrandMonitorTestTask.Infrastructure;
using Dapper;
using MediatR;

namespace BrandMonitorTestTask.DataAccess.Handlers.V1;

using static GetRecordsForUpdate;

public class GetRecordsForUpdateHandler : IRequestHandler<Requst, Response>
{
    private static readonly string QueryForRun = @$"
        SELECT
            id
        FROM {PgTables.Record}
        WHERE status = '{RecordStatus.Created}'::record_status;";
    
    private static readonly string QueryForFinish = @$"
        SELECT
            id
        FROM {PgTables.Record}
        WHERE status = '{RecordStatus.Running}'::record_status AND updated_at <= @DateFrom;";

    private readonly IDbContext _dbContext;

    public GetRecordsForUpdateHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(Requst requst, CancellationToken cancellationToken)
    {
        await using var dbConnection = _dbContext.GetConnection();
        await dbConnection.OpenAsync(cancellationToken);

        var reader = await dbConnection.QueryMultipleAsync(QueryForRun + QueryForFinish, requst.AsParameters);

        var idsForRun = await reader.ReadAsync<Guid>();
        var idsForFinish = await reader.ReadAsync<Guid>();

        return new Response(idsForRun.ToArray(), idsForFinish.ToArray());
    }
}