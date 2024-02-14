using BrandMonitorTestTask.Infrastructure;
using MediatR;

namespace BrandMonitorTestTask.DataAccess.Handlers.V1;

public class SetStatus
{
    public record Request(IReadOnlyCollection<Guid> Ids, RecordStatus Status) : IRequest
    {
        public readonly object AsParameters = new
        {
            Ids = Ids.ToArray(),
            Status = Status.ToString(),
            DateNow = DateTime.Now
        };
    }
}