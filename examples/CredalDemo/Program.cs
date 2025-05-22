// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-20
// Source: https://github.com/lede701/Credal.Net
//
// Credal demo app.  This code is not intended for production use and is only an example of how the Credal API tools can be used.
// There is no support for this code and it is not guaranteed to work in all environments.  Use at your own risk.

using Credal.Net.Clients;

// Create a basic chat manager object, these parameters should be part of a configuration file or environment variables and never hard coded.
var chat = new CredalClient(Guid.Parse("00000000-0000-0000-0000-000000000000")
    , "<user email address>"
    , "<API KEY>");


// Initialize the console input
string query = string.Empty;
string quitCommand = "EXIT";
// Run until the user types the quitCommand
while (query.ToUpper() != quitCommand)
{
    Console.WriteLine("---------------------------------------------");
    Console.WriteLine("Ask your question or type 'exit' to quit.");
    Console.Write("Ask: ");
    query = Console.ReadLine() ?? string.Empty;

    if (query.ToUpper() != quitCommand)
    {
        var response = await chat.SendMessageAsync(query);
        if (response.IsSuccess)
        {
            Console.WriteLine($"AI: {response.Result!.Response.Message}");
        }
        else
        {
            Console.WriteLine("AI: I'm unable to answer this question.");
        }
    }

    Console.WriteLine("---------------------------------------------\r\n");
}