using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctionBackend;

public class FunctionGetParticipants(ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<FunctionGetEvents>();

    [Function("FunctionGetParticipants")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req, string eventId)
    {
        if (string.IsNullOrWhiteSpace(eventId))
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
        string json = JsonSerializer.Serialize(await new MeetupEventService().GetParticipantsForEvent(eventId));
        await response.WriteStringAsync(json);

        return response;
    }
}