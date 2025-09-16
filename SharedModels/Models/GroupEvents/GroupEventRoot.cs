using System.Text.Json.Serialization;

namespace SharedModels.Models.GroupEvents;
#nullable disable
public class GroupEventRoot
{
    [JsonPropertyName("events")]
    public EventList Events { get; set; }
}