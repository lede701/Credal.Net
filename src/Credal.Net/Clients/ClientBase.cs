// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-21
// Source: https://github.com/lede701/Credal.Net

using Credal.Net.Config;
using Credal.Net.Results;
using Credal.Net.Security;
using System.Text;
using System.Text.Json;

namespace Credal.Net.Clients
{
    public class ClientBase
    {
        public Autherization Auth { get; set; }
        public ClientHeaders Headers { get; set; }
        public EndpointConfig Endpoint { get; set; }
        private readonly HttpClient? _client;

#if NET8_0_OR_GREATER
        public DateTime Now { get => TimeProvider.System.GetLocalNow().DateTime; }
        public DateTime UtcNow { get => TimeProvider.System.GetUtcNow().DateTime; }
#else
public DateTime Now { get => DateTime.Now; }
public DateTime UtcNow { get => DateTime.UtcNow; }
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
        /// <param name="wrapResults">Are the return results wrapped or unwrapped? Default: false</param>
        /// <returns>Returns wrapped CredalResult object based on the TResponse specified class.</returns>
        public async Task<CredalResult<TResponse>> Send<TResponse, TRequest>(TRequest payload, bool wrapResults = false) where TResponse : class
        {
            if (this._client is not null)
            {
                return await this.Send<TResponse, TRequest>(this._client, payload, wrapResults);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    return await this.Send<TResponse, TRequest>(client, payload, wrapResults);
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
            else if(response is not null)
            {
                // Create a error trail to help diagnose connection issue
                var e = CredalResult<TResponse>.Failure;
                e.Error!.Message = response.RequestMessage?.ToString() ?? "No error returned from server";
                e.Error.StatusCode = (int)response.StatusCode;

                var errorContent = await response.Content.ReadAsStringAsync();
                e.Error.ResponseBody = errorContent;
                e.Error.RequestUri = response.RequestMessage?.RequestUri?.ToString();
                e.Error.RequestMethod = response.RequestMessage?.Method?.ToString();
                e.Error.ReasonPhrase = response.ReasonPhrase;
                e.Error.Timestamp = this.UtcNow;
                e.Error.Headers = response.Headers.ToDictionary(h => h.Key, h => string.Join(", ", h.Value));

                return e;
            }

            return CredalResult<TResponse>.Failure;
        }
    }
}