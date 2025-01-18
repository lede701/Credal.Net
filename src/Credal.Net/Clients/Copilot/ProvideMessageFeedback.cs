﻿using Credal.Net.Clients;
using Credal.Net.Config;
using Credal.Net.Core.Config;
using Credal.Net.Models;
using Credal.Net.Results;
using Credal.Net.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credal.Net.Copilot;

public class ProvideMessageFeedback : ClientBase
{
    public Guid AgentId { get; set; }
    public string? UserEmail { get; set; }
    public bool IsDefaultValid { get => this.AgentId != Guid.Empty && !string.IsNullOrEmpty(this.UserEmail); }

    public ProvideMessageFeedback(EndpointConfig endpoint, Autherization autherization) : base(autherization, new ClientHeaders(), endpoint) { }

    public ProvideMessageFeedback(EndpointConfig endpoint, ClientHeaders headers, Autherization autherization) : base(autherization, headers, endpoint) { }
    public ProvideMessageFeedback(EndpointConfig endpoint, ClientHeaders headers, Autherization autherization, HttpClient client) : base(autherization, headers, endpoint, client) { }

    public async Task<CredalResult<ProvideMessageFeedbackResult>> SendAsync(string message, Guid messageId)
    {
        if (this.IsDefaultValid)
        {
            await this.SendAsync(new ProvideMessageFeedbackModel(this.AgentId, this.UserEmail!, messageId, MessageFeedbackModel.Positive));
            return CredalResult<ProvideMessageFeedbackResult>.Success(new ProvideMessageFeedbackResult());
        }
        return CredalResult<ProvideMessageFeedbackResult>.Failure;
    }

    public async Task<CredalResult<ProvideMessageFeedbackResult>> SendAsync(ProvideMessageFeedbackModel feedback)
    {
        return await this.Send<ProvideMessageFeedbackResult, ProvideMessageFeedbackModel>(feedback);
    }

}
