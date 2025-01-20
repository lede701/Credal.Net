// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-20
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
    private readonly HttpClient _client;

    public ClientBase(Autherization auth) : this(auth, new ClientHeaders(), new EndpointConfig()) { }

    public ClientBase(Autherization auth, ClientHeaders headers, EndpointConfig endpoint)
    {
        this.Auth = auth;
        this.Headers = headers;
        this.Endpoint = endpoint;
        this._client = new HttpClient();
    }

    public ClientBase(Autherization auth, ClientHeaders headers, EndpointConfig endpoint, HttpClient client) : this(auth, headers, endpoint)
    {
        this._client = client;
    }

    public async Task<CredalResult<TResponse>> Send<TResponse, TRequest>(TRequest message, bool resultsNotWrapped = false) where TResponse : class
    {
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(this.Auth.SecurityType, this.Auth.ApiKey);
        if (this.Headers.HasHeaders)
        {
            foreach (var head in this.Headers.KeyValuePairs)
            {
                _client.DefaultRequestHeaders.Add(head.Key, head.Value);
            }
        }
        var jsonContent = JsonSerializer.Serialize(message);
        HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        switch (this.Endpoint.HttpMethod.ToUpper())
        {
            case "DELETE":
                {
                    var request = new HttpRequestMessage(HttpMethod.Delete, this.Endpoint.Uri)
                    {
                        Content = content
                    };
                    var response = await _client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResults = await response.Content.ReadAsStringAsync();
                        if(resultsNotWrapped)
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
            case "GET":
                {
                }
                break;
            case "POST":
                {
                    var response = await _client.PostAsync(this.Endpoint.Uri, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResults = await response.Content.ReadAsStringAsync();
                        if(resultsNotWrapped)
                        {
                            TResponse? simpleResult = JsonSerializer.Deserialize<TResponse>(jsonResults);
                            return new CredalResult<TResponse>() { Result = simpleResult };
                        }
                        else
                        {
                            var result = JsonSerializer.Deserialize<CredalResult<TResponse>>(jsonResults);
                            return result ?? CredalResult<TResponse>.Failure;
                        }
                    }
                }
                break;
            case "UPDATE":
                {
                }
                break;
            default:
                {
                    throw new NotImplementedException($"Http method {this.Endpoint.HttpMethod} is not supported.");
                }
         }

        return CredalResult<TResponse>.Failure;
    }
}
