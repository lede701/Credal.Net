using Credal.Net.Config;
using Credal.Net.Core.Config;
using Credal.Net.Models;
using Credal.Net.Results;
using Credal.Net.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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

    public async Task<CredalResult<TResponse>> Send<TResponse, TRequest>(TRequest message) where TResponse : class
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

                }
                break;
            case "GET":
                {
                }
                break;
            case "POST":
                {
                    var response = await _client.PostAsync(this.Endpoint.Uri, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<CredalResult<TResponse>>();
                        return result ?? CredalResult<TResponse>.Failure;
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
