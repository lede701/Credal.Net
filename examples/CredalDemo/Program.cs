// See https://aka.ms/new-console-template for more information
using Credal.Net.Copilot;
using Credal.Net.Security;

var chat = new SendMessage(new Credal.Net.Config.EndpointConfig(), new Autherization()
{
    ApiKey = "<API_KEY>"
})
{
    AgentId = Guid.Parse("00000000-0000-0000-0000-000000000000"),
    UserEmail = "<userEmailAddress>"
};

var response = await chat.SendAsync("The question to ask your Copilot?");

if (response.IsSuccess)
{
    var result = response.Results;
    Console.WriteLine(result?.Response.Message ?? "No response was available");
}