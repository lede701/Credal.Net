# Credal.Net

This library is for use with the Credal AI API services. You can use this to access your Copilots in any .Net 8 project.

## Usage

First you need to create an API key on Credal and associate it with any trained Copilot you have created. Currently all commands for the API
are async methods so you will need an unwrapper to get the results in non async methods.

Install Credal.Net from Nuget

Create an instance of the CredalClient class.
1. Pass the Unique Id of your copilot through the constructor
1. Pass a valid user email address.
1. Pass your API key through the contructor with a valid email address associated with the API key.


```csharp
using Credal.Net.Client

var client = new CredalClient("copilot-id", "user-email", "api-key");

var credalModel = await client.SendMessageAsync("your-question-for-copilot");

if(credalModel.IsSuccess){
	Console.WriteLine(credalModel.Result.Response.Message);
}

Please check out the console demo to see a more detailed example.