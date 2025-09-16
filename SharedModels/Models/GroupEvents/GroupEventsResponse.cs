using System.Text.Json.Serialization;

namespace SharedModels.Models.GroupEvents;
#nullable disable
public class GroupEventsResponse
{
    [JsonPropertyName("groupByUrlname")]
    public GroupEventRoot Root { get; set; }

    public IReadOnlyList<Event> GetAllEvents()
    {
        var result = Root.Events.Edges.Select(x => x.Node).ToList();
        return result.OrderByDescending(x => x.Date).ToList();
    }
}