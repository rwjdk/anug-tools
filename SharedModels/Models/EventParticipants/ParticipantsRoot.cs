using System.Text.Json.Serialization;

namespace SharedModels.Models.EventParticipants;
#nullable disable
public class ParticipantsRoot
{
    [JsonPropertyName("rsvps")]
    public ParticipantList Tickets { get; set; }
}