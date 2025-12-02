using SharedModels.Models.EventParticipants;
using SharedModels.Models.GroupEvents;
using System.Text.Json;

namespace BlazorApp.Services;

public class BackendCaller(HttpClient httpClient)
{
    public async Task<IReadOnlyList<Event>?> GetEvents()
    {
        string endpoint = "https://anugtoolsbackend.azurewebsites.net/api/FunctionGetEvents?"; //Open for CORS https://localhost:7112
#if DEBUG
        endpoint = "http://localhost:7083/api/FunctionGetEvents";
#endif
        string json = await httpClient.GetStringAsync(endpoint);
        List<Event>? events = JsonSerializer.Deserialize<List<Event>>(json);
        return events;
    }

    public async Task<List<Participant>?> GetParticipants(Event @event)
    {
        string endpoint = $"https://anugtoolsbackend.azurewebsites.net/api/FunctionGetParticipants?eventId={@event.Id}"; //Open for CORS https://localhost:7112
#if DEBUG
        endpoint = $"http://localhost:7083/api/FunctionGetParticipants?eventId={@event.Id}";
#endif
        string json = await httpClient.GetStringAsync(endpoint);
        return JsonSerializer.Deserialize<List<Participant>>(json);
    }
}