using Newtonsoft.Json;
using RestSharp;
using SupabaseAuthAPI.Modesl;
using System.Net;


namespace SupabaseAuthAPI.Services;

public class SupabaseAuthService
{
    private readonly string _supabaseUrl;
    private readonly string _supabaseKey;

    public SupabaseAuthService(string supabaseUrl, string supabaseKey)
    {
        _supabaseUrl = supabaseUrl;
        _supabaseKey = supabaseKey;
    }

    public async Task<SupabaseAuthResponse> LoginAsync(LoginRequest loginRequest)
    {
        // Input validation
        
        if (loginRequest == null)
        {
            throw new ArgumentNullException(nameof(loginRequest));
        }

        if (string.IsNullOrEmpty(loginRequest.Email))
        {
            throw new ArgumentException("Email is required", nameof(loginRequest));
        }

        if (string.IsNullOrEmpty(loginRequest.Password))
        {
            throw new ArgumentException("Password is required", nameof(loginRequest));
        }

        // Create a new RestSharp client
        var client = new RestClient(_supabaseUrl);

        // Create a new RestSharp request
        var request = new RestRequest("auth/v1/token?grant_type=password", Method.Post);

        // Add headers
        request.AddHeader("apikey", _supabaseKey);
        request.AddHeader("Content-Type", "application/json");

        // Add body
        request.AddJsonBody(loginRequest);

        // Execute the request
        var response = await client.ExecuteAsync(request);

        // Check if the request was successful
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new Exception($"Failed to login. Status code: {response.StatusCode}");
        }

        // Deserialize the response
        var supabaseAuthResponse = JsonConvert.DeserializeObject<SupabaseAuthResponse>(response.Content);

        return supabaseAuthResponse;
    }
}
