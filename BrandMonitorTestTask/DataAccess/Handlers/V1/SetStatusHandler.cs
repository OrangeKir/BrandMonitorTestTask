using BrandMonitorTestTask.Infrastructure;
using Dapper;
using MediatR;

namespace BrandMonitorTestTask.DataAccess.Handlers.V1;

using static SetStatus;

public class SetStatusHandler : IRequestHandler<Request>
{
    private const string Query = @$"
        UPDATE {PgTables.Record}
        SET 
            status = @Status::record_status,
            updated_at = @DateNow
        WHERE id = ANY(@Ids)";
    
    private readonly IDbContext _dbContext;

    public SetStatusHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(Request request, CancellationToken cancellationToken)
    {
        await using var dbConnection = _dbContext.GetConnection();
        await dbConnection.OpenAsync(cancellationToken);
        
        await dbConnection.ExecuteAsync(Query, request.AsParameters);
    }
}