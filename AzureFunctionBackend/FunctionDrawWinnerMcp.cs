using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using Microsoft.Extensions.Logging;
using SharedModels.Models.EventParticipants;
using SharedModels.Models.GroupEvents;
using SharedModels.Services;

namespace AzureFunctionBackend;

public class FunctionDrawWinnerMcp(MeetupEventService meetupEventService, ParticipationWinnerSelector participationWinnerSelector)
{
    [Function(Constants.Name)]
    public async Task<IActionResult> Run(
        [McpToolTrigger(Constants.ToolName, Constants.ToolDescription)]
        ToolInvocationContext context)
    {
        IReadOnlyList<Event> events = await meetupEventService.GetEvents();
        Event? @event = events.OrderByDescending(x => x.Date).FirstOrDefault(x => x.Date <= DateTime.Today.AddDays(1));
        if (@event == null)
        {
            return new BadRequestObjectResult("No events found");
        }

        IReadOnlyList<Participant> participants = await meetupEventService.GetParticipantsForEvent(@event.Id);

        participationWinnerSelector.MarkParticipantsAsWinners(participants, false, 1);

        Participant? winner = participants.FirstOrDefault(x => x.Winner);
        if (winner == null)
        {
            return new BadRequestObjectResult("No winner found");
        }

        return new OkObjectResult($"And the Winner is [{winner.Name}]({winner.Url}). Congratulations!");
    }
}