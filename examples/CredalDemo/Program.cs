// See https://aka.ms/new-console-template for more information
using Credal.Net.Copilot;

var chat = new ChatManager(Guid.Parse("00000000-0000-0000-0000-000000000000")
    , "<user email address>"
    , "<API Key>");

var response = await chat.SendMessageAsync("<Query to ask CoPilot>");

Console.WriteLine(response.Results!.Response.Message);
Console.WriteLine(response.Results!.ConversationId);