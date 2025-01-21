// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-21
// Source: https://github.com/lede701/Credal.Net

using Credal.Net.Config;
using Credal.Net.Core.Config;
using Credal.Net.Results;
using Credal.Net.Security;
using System.Text;
using System.Text.Json;

namespace Credal.Net.Clients;

public class ClientBase
{
    public Autherization Auth { get; set; }
    public ClientHeaders Headers { get; set; }
    public EndpointConfig Endpoint { get; set; }
    private readonly HttpClient? _client;

#if NET_8_0_OR_GRATER
    public DateTime Now { get => TimeProvider.System.GetLocalNow().DateTime; }
#else
    public DateTime Now { get => DateTime.Now; }
#endif

    public ClientBase(Autherization auth) : this(auth, new ClientHeaders(), new EndpointConfig()) { }

    public ClientBase(Autherization auth, ClientHeaders headers, EndpointConfig endpoint)
    {
        this.Auth = auth;
        this.Headers = headers;
        this.Endpoint = endpoint;
        this._client = null;
    }

    /// <summary>
    /// This constructor is primarly used for unit testing since the HttpClient need to be mocked.
    /// </summary>
    /// <param name="auth"></param>
    /// <param name="headers"></param>
    /// <param name="endpoint"></param>
    /// <param name="client"></param>
    public ClientBase(Autherization auth, ClientHeaders headers, EndpointConfig endpoint, HttpClient client) : this(auth, headers, endpoint)
    {
        this._client = client;
    }

    /// <summary>
    /// Send a message to the API endpoint
    /// </summary>
    /// <typeparam name="TResponse">Response type expected as the return result</typeparam>
    /// <typeparam name="TRequest">Request type that is being sent</typeparam>
    /// <param name="payload">Objec that is expected to be sent to the API endpoint.</param>
    /// <param name="resultsNotWrapped">Are the return results wrapped or unwrapped? Default: false</param>
    /// <returns>Returns wrapped CredalResult object based on the TResponse specified class.</returns>
    public async Task<CredalResult<TResponse>> Send<TResponse, TRequest>(TRequest payload, bool resultsNotWrapped = false) where TResponse : class
    {
        if (this._client is not null)
        {
            return await this.Send<TResponse, TRequest>(this._client, payload, resultsNotWrapped);
        }
        else
        {
            using (var client = new HttpClient())
            {
                return await this.Send<TResponse, TRequest>(client, payload, resultsNotWrapped);
            }
        }
    }

    private async Task<CredalResult<TResponse>> Send<TResponse, TRequest>(HttpClient client, TRequest message, bool resultsNotWrapped = false) where TResponse : class
    {

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(this.Auth.SecurityType, this.Auth.ApiKey);
        if (this.Headers.HasHeaders)
        {
            foreach (var head in this.Headers.KeyValuePairs)
            {
                client.DefaultRequestHeaders.Add(head.Key, head.Value);
            }
        }
        var jsonContent = JsonSerializer.Serialize(message);
        HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        HttpResponseMessage? response = null;
        switch (this.Endpoint.HttpMethod.ToUpper())
        {
            case "DELETE":
                {
                    var request = new HttpRequestMessage(HttpMethod.Delete, this.Endpoint.Uri)
                    {
                        Content = content
                    };
                    response = await client.SendAsync(request);
                    break;
                }
            case "GET":
                {
                }
                break;
            case "POST":
                {
                    response = await client.PostAsync(this.Endpoint.Uri, content);
                }
                break;
            case "PATCH":
                {
                    var request = new HttpRequestMessage(HttpMethod.Patch, this.Endpoint.Uri)
                    {
                        Content = content
                    };
                    response = await client.SendAsync(request);
                }
                break;
            default:
                {
                    throw new NotImplementedException($"Http method {this.Endpoint.HttpMethod} is not supported.");
                }
         }
        if (response is not null && response.IsSuccessStatusCode)
        {
            var jsonResults = await response.Content.ReadAsStringAsync();
            if (resultsNotWrapped)
            {
                return new CredalResult<TResponse>() { Result = JsonSerializer.Deserialize<TResponse>(jsonResults) };
            }
            else
            {
                var result = JsonSerializer.Deserialize<CredalResult<TResponse>>(jsonResults);
                return result ?? CredalResult<TResponse>.Failure;
            }
        }

        return CredalResult<TResponse>.Failure;
    }
}
