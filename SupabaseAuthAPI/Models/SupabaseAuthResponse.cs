using Newtonsoft.Json;

namespace SupabaseAuthAPI.Modesl;

public class SupabaseAuthResponse
{
    [JsonProperty("access_token")]
    public required string AccessToken { get; set; }

    [JsonProperty("refresh_token")]
    public required string RefreshToken { get; set; }
}
