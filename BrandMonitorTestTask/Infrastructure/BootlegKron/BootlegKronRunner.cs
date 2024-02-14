using BrandMonitorTestTask.DataAccess.Handlers.V1;
using MediatR;

namespace BrandMonitorTestTask.Infrastructure.BootlegKron;

public class BootlegKronRunner : IHostedService
{
    private readonly IMediator _mediator;

    public BootlegKronRunner(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (true)
        {
            await ProcessInternalAsync(cancellationToken);
            await Task.Delay(10000, cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task ProcessInternalAsync(CancellationToken cancellationToken)
    {
        var getRequest = new GetRecordsForUpdate.Requst();
        var ids = await _mediator.Send(getRequest, cancellationToken);

        var setRunStatusRequest = new SetStatus.Request(ids.IdsForRun, RecordStatus.Running);
        await _mediator.Send(setRunStatusRequest, cancellationToken);
        
        var setRunFinishRequest = new SetStatus.Request(ids.IdsForFinish, RecordStatus.Finished);
        await _mediator.Send(setRunFinishRequest, cancellationToken);
        
    }
}