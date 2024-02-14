using MediatR;

namespace BrandMonitorTestTask.DataAccess.Handlers.V1;

public class CreateRecord
{
    public record Request : IRequest<Response>
    {
        public readonly object AsParameters = new
        {
            DateNow = DateTime.Now
        };
    }

    public record Response(Guid Id);
}