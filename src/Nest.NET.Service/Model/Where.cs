using Newtonsoft.Json;

namespace Nest.NET.Service.Model;

public class Where
{
    [JsonProperty("where_id")]
    public string Id { get; set; } = null!;
    public string? Name { get; set; }
}