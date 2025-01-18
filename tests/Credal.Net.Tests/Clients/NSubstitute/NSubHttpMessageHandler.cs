using System.Net;

namespace Credal.Net.Tests.Clients.NSubstitute;

public class NSubHttpMessageHandler : HttpMessageHandler
{
    private readonly string _responseContent;
    private readonly HttpStatusCode _statusCode;

    public NSubHttpMessageHandler()
    {
        _responseContent = "No defined content provided";
        _statusCode = HttpStatusCode.OK;
    }

    public NSubHttpMessageHandler(string responseContent)
    {
        _responseContent = responseContent;
        _statusCode = HttpStatusCode.OK;
    }

    public NSubHttpMessageHandler(string responseContent, HttpStatusCode statusCode)
    {
        _responseContent = responseContent;
        _statusCode = statusCode;
    }

    public async Task<HttpResponseMessage> NSubSendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return await this.SendAsync(request, cancellationToken);
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.Content is not null)
        {
            var input = await request.Content.ReadAsStringAsync();
        }
        return new HttpResponseMessage()
        {
            StatusCode = _statusCode,
            Content = new StringContent(_responseContent)
        };
    }
}
