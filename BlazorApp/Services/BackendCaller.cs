using SharedModels.Models.EventParticipants;
using SharedModels.Models.GroupEvents;
using System.Text.Json;

namespace BlazorApp.Services;

public class BackendCaller(HttpClient httpClient)
{
    public async Task<IReadOnlyList<Event>?> GetEvents()
    {
        string endpoint = "api/FunctionGetEvents";
        string json = await httpClient.GetStringAsync(endpoint);
        List<Event>? events = JsonSerializer.Deserialize<List<Event>>(json);
        return events;
    }

    public async Task<List<Participant>?> GetParticipants(Event @event)
    {
        string endpoint = $"api/FunctionGetParticipants?eventId={Uri.EscapeDataString(@event.Id)}";
        string json = await httpClient.GetStringAsync(endpoint);
        return JsonSerializer.Deserialize<List<Participant>>(json);
    }
}
