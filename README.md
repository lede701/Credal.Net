# Credal.Net

This library is for use with the Credal AI API services. You can use this to access your Copilots in any .Net 6.0+ project.

## Usage

First you need to create an API key on Credal and associate it with any trained Copilot you have created. Currently all commands for the API
are async methods so you will need an unwrapper to get the results in non async methods.

Install Credal.Net from Nuget

Create an instance of the CredalClient class.
1. Pass the Unique Id of your copilot through the constructor
1. Pass a valid user email address.
1. Pass your API key through the constructor with a valid email address associated with the API key.


```csharp
using Credal.Net.Client

var client = new CredalClient("copilot-id", "user-email", "api-key");

var credalModel = await client.SendMessageAsync("your-question-for-copilot");

if(credalModel.IsSuccess){
	Console.WriteLine(credalModel.Result.Response.Message);
}
```

Please check out the console demo to see a more detailed example.

## Updates
### 0.0.7
- Adding better error response information.
- Fixed bug not using time provider in newer .net versions

### 0.0.6
- Reorginized the result and model namepsace.
- Change namespace definition to kind of help support older .Net versions.
- Added Document Catalog endpoints to CredalClient.
- Added Action endpoint to CredalClient.

### 0.0.5
- Added ConversationException properly report failed Conversation feedback.
- Refactored the HttpClient process so a single client isn't always used and will auto clean up after each usage.  A HttpClient can still be injected for long term use if needed.
- Updated CredalClient and added documentation to methods.
- Updated CredalClient by marking beta features as such.  These are features in Credal that are marked beta and may not be fully supported.
- Optimized the ClientBase class on how it creates the return results.
- Added support for .Net 6.0 - .Net 9.0 libraries.

