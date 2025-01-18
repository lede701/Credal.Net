using Credal.Net.Config;
using Credal.Net.Copilot;
using Credal.Net.Core.Config;
using Credal.Net.Models;
using Credal.Net.Results;
using Credal.Net.Security;
using Credal.Net.Tests.Clients.NSubstitute;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Credal.Net.Tests.Clients.Copilot;

public class ChatClientTests
{
    private readonly Autherization _nsubAuth;
    private readonly ClientHeaders _nsubHeaders;
    private readonly EndpointConfig _nsubEndpoint;

    public ChatClientTests()
    {
        _nsubAuth = Substitute.For<Autherization>();
        _nsubHeaders = Substitute.For<ClientHeaders>();
        _nsubEndpoint = Substitute.For<EndpointConfig>();

        _nsubAuth.ApiKey.Returns("test-api-key");
        _nsubAuth.SecurityType.Returns("Bearer");

        _nsubHeaders.HasHeaders.Returns(false);

        _nsubEndpoint.HttpMethod.Returns("POST");
        _nsubEndpoint.Uri.Returns("https://api.test.ai/api/v0/copilots");

    }

    [Fact]
    public void ChatClient_AlwaysSuccess()
    {
        Assert.True(true);
    }

    [Fact]
    public async Task Send_PostRequest_ReturnSuccessResult()
    {
        // Arrange
        var message = new Models.SendMessageModel(Guid.NewGuid(), "Test Message", "test@test.ai");
        var expectedResponse = new CredalResult<string> { Results = "Success" };
        var nsubHandler = Substitute.For<NSubHttpMessageHandler>();
        nsubHandler.NSubSendAsync(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(expectedResponse))
            }));
        var nsubClient = Substitute.For<HttpClient>(nsubHandler);
        var http = new HttpClient(nsubHandler);
        var client = new SendMessage(_nsubEndpoint, _nsubHeaders, _nsubAuth, nsubClient);

        // Act
        var result = await client.Send<SendChatResult, SendMessageModel>(message);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Results);
        Assert.True(result.IsSuccess);
        Assert.Equal("Success", result.Results!.Response.Message);
    }
}


