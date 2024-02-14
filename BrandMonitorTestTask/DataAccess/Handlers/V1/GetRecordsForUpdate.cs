using MediatR;

namespace BrandMonitorTestTask.DataAccess.Handlers.V1;

public class GetRecordsForUpdate
{
    public record Requst : IRequest<Response>
    {
        public readonly object AsParameters = new
        {
            DateFrom = DateTime.Now.AddMinutes(-2)
        };
    }

    public record Response(IReadOnlyCollection<Guid> IdsForRun, IReadOnlyCollection<Guid> IdsForFinish);
}