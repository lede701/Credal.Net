using Credal.Net.Config;
using Credal.Net.Copilot;
using Credal.Net.Core.Config;
using Credal.Net.Models;
using Credal.Net.Results;
using Credal.Net.Security;
using FluentAssertions;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;

namespace Credal.Net.Tests.Copilot;

[TestFixture]
public class SendMessageTests
{
    private Mock<Autherization> _mockAuth;
    private Mock<ClientHeaders> _mockHeaders;
    private EndpointConfig _config;
    private Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private Mock<HttpClient> _mockHttpClient;

    [SetUp]
    public void Setup()
    {
        _mockAuth = new Mock<Autherization>();
        _mockHeaders = new Mock<ClientHeaders>();
        _config = new EndpointConfig()
        {
            Endpoint = "https://api.testuri.ai/api"
        };

        _mockHttpClient = new Mock<HttpClient>();
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();

    }

    [Test]
    public void SendMessage_SanityCheck()
    {
        Assert.That(true, Is.True);
    }

    [Test]
    public async Task SendMessage_WhenMessageIsSent_ShouldReturnSuccess()
    {
        // Arrange
        var expectedResult = CredalResult<SendChatResult>.Success(new SendChatResult()
        {
            ConversationId = Guid.NewGuid()
            , Response = new ResponseResult()
            {
                Message = "Hello back to you",
                DataChunk = "data-chunk"
            },

        });

        var httpResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(expectedResult))
        };

        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(httpResponse);

        var httpClient = new HttpClient(_mockHttpMessageHandler.Object);
        var client = new SendMessage(_config, _mockHeaders.Object, _mockAuth.Object, httpClient);
        var message = new SendMessageModel(Guid.NewGuid(), "Hello bot", "demo@testemail.com");


        // Act
        var result = await client.SendAsync(message);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CredalResult<SendChatResult>>();
        result.IsSuccess.Should().BeTrue();
        result.Result.Should().NotBeNull();
        result.Result.Response.Message.Should().Be(expectedResult.Result!.Response.Message);
    }
}
