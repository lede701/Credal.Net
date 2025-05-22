// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-21
// Source: https://github.com/lede701/Credal.Net

using Credal.Net.Actions;
using Credal.Net.Config;
using Credal.Net.Copilot;
using Credal.Net.DocumentCatalog;
using Credal.Net.Exceptions;
using Credal.Net.Models;
using Credal.Net.Models.Action;
using Credal.Net.Models.Copilot;
using Credal.Net.Models.DocumentCatalog;
using Credal.Net.Results;
using Credal.Net.Results.Action;
using Credal.Net.Results.Copilot;
using Credal.Net.Results.DocumentCatalog;
using Credal.Net.Security;

namespace Credal.Net.Clients
{
    public class CredalClient
    {
        private readonly Guid _agentId;
        private readonly string _userEmail;
        private Guid? _conversationId { get; set; }
        private readonly EndpointConfig _config;
        private readonly Autherization _auth;

        /// <summary>
        /// Credal API client for connecting to their AI engine.
        /// </summary>
        /// <param name="agentId">Unique ID of Copilot</param>
        /// <param name="userEmail">Valid user email address</param>
        /// <param name="apiKey">Credal API generated key</param>
        public CredalClient(Guid agentId, string userEmail, string apiKey)
        {
            _agentId = agentId;
            _userEmail = userEmail;
            _config = new EndpointConfig();
            _auth = new Autherization()
            {
                ApiKey = apiKey,
            };
        }

        /// <summary>
        /// Call to Invoke an Action, asking for human confirmation if necessary. This is a beta feature and may change in future releases.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [Obsolete("This class is in beta and may change in future releases.")]
        public Task<CredalResult<InvokeActionResult>> InvokeActionAsync(InvokeActionModel action)
        {
            var client = new InvokeAction(_config, _auth);
            return client.SendAsync(action);
        }

        #region Methods: Copilot

        /// <summary>
        /// Copilot API call to query the trained copilot for a response. ConversationId will get update or created when the results are returned and successful.
        /// </summary>
        /// <param name="query">Query to ask the copilot</param>
        /// <returns>CredalRespon wrapped result that contains the SendChatResult object.  Check the IsSuccessful parameter in the result for validation before using the result value.</returns>
        public async Task<CredalResult<SendChatResult>> SendMessageAsync(string query)
        {
            var client = new SendMessage(_config, new ClientHeaders(), _auth);

            var msgModel = new SendMessageModel(_agentId, query, _userEmail);
            if (_conversationId is not null && _conversationId != Guid.Empty)
            {
                msgModel.ConversationId = _conversationId.ToString();
            }

            var response = await client.SendAsync(msgModel);
            if (response.IsSuccess
                && response.Result is not null)
            {
                if (_conversationId is null
                    || _conversationId == Guid.Empty
                    || _conversationId != response.Result.ConversationId)
                {
                    // Set new conversation idfor manager to use with tracking
                    _conversationId = response.Result.ConversationId;
                }

            }

            return response;
        }

        /// <summary>
        /// Create a new conversation with the copilot. This is optional as the SendMessageAsync method will create a new conversationId when the results are returned.
        /// </summary>
        /// <returns>Return true on success and false on failure of creating a conversatioinId</returns>
        public async Task<bool> CreateConversationAsync()
        {
            var client = new CreateConversation(_config, _auth);

            var response = await client.SendAsync(new AgentEmailModel(_agentId, _userEmail));
            if (response.IsSuccess
                && response.Result is not null)
            {
                _conversationId = response.Result.ConversationId;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Provide feedback on messages sent from copilot. This is optional and can be used to help train the copilot.
        /// </summary>
        /// <param name="success">Is this a positive repsone from copilot.</param>
        /// <param name="message">Optional feedback response about the answer provided.</param>
        /// <returns></returns>
        /// <exception cref="ConversationException">Thrown when no conversationId is available to provide feedback on.</exception>
        public async Task ProvideFeedbackAsync(bool success, string? message)
        {
            if (_conversationId is null)
            {
                throw new ConversationException("Conversation not created yet.");
            }
            var client = new ProvideMessageFeedback(_config, _auth);
            var feedback = new ProvideMessageFeedbackModel(_agentId
                , _userEmail
                , _conversationId.Value
                , success ? MessageFeedbackModel.Positive : MessageFeedbackModel.Negative);

            var response = await client.SendAsync(feedback);
        }

        /// <summary>
        /// Create a new copilot for the provided user address. This is a beta feature and may change in future releases.
        /// </summary>
        /// <param name="name">Copilot name</param>
        /// <param name="description">Description of the copilot</param>
        /// <returns>The AgentId or UniqueId in Credal of the new copilot.</returns>
        [Obsolete("This class is in beta and may change in future releases.")]
        public async Task<Guid> CreateCopilotAsync(string name, string description)
        {
            var client = new CreateCopilot(_config, _auth);
            var results = await client.SendAsync(new CreateCopilotModel(name, description, new List<CollaberatorModel>()
        {
            new CollaberatorModel(_userEmail, "editor")
        }));

            if (results.IsSuccess
                && results.Result is not null)
            {
                return results.Result.AgentId;
            }

            return Guid.Empty;
        }

        /// <summary>
        /// Delete a copilot from the user email address provided. This is a beta feature and may change in future releases.
        /// </summary>
        /// <param name="copilotId">AgentId or UniqueId of copilot in the Credal system.</param>
        /// <returns>True on successful deletion</returns>
        [Obsolete("This class is in beta and may change in future releases.")]
        public async Task<bool> DeleteCopilotAsync(Guid copilotId)
        {
            var client = new DeleteCopilot(_config, _auth);
            var results = await client.SendAsync(new DeleteCopilotModel(copilotId));
            return copilotId == results!.Result!.CopilotId;
        }

        #endregion

        #region Methods: Document Catalog

        public async Task<CredalResult<UploadDocumentContentsResult>> UploadDocumentContentsAsync(UploadDocumentContentsModel documents)
        {
            var client = new UploadDocumentContents(_config, _auth);
            return await client.SendAsync(documents);
        }

        [Obsolete("This class is in beta and may change in future releases.")]
        public async Task<CredalResult<SyncSourceByUrlResult>> SyncSoiurceByUrlAsync(SyncSourceByUrlModel source)
        {
            var client = new SyncSourceByUrl(_config, _auth);
            return await client.SendAsync(source);
        }

        public async Task<CredalResult<CatalogMetadataResult>> CatalogMetadataAsync(CatalogMetadataModel metadata)
        {
            var client = new CatalogMetadata(_config, _auth);
            return await client.SendAsync(metadata);
        }

        #endregion
    }
}