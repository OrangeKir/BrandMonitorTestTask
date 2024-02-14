using BrandMonitorTestTask.DataAccess.Handlers.V1;
using BrandMonitorTestTask.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BrandMonitorTestTask.Controllers.V1;

[ApiController]
[Route("v1")]
public class TestController : ControllerBase
{
    private readonly IMediator _mediator;

    public TestController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet("task/{idString}")]
    public async Task<GetRecord.Response> GetRecordAsync(string idString)
    {
        try
        {
            var id = Guid.Parse(idString);
            return await _mediator.Send(new GetRecord.Request(id));
        }
        catch (FormatException)
        {
            Response.StatusCode = 400;
            return new GetRecord.Response(RecordStatus.Default);
        }
    }

    [HttpPut("task")]
    public Task<CreateRecord.Response> TestCreateAsync(CreateRecord.Request request)
        => _mediator.Send(request);
}