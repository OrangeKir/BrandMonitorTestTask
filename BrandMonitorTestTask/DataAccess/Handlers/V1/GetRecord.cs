using BrandMonitorTestTask.Infrastructure;
using MediatR;

namespace BrandMonitorTestTask.DataAccess.Handlers.V1;

public class GetRecord
{
    public record Request(Guid Id) : IRequest<Response>;

    public record Response(RecordStatus Status);
}