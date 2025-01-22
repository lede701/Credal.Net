using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Credal.Net.Models.DocumentCatalog
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CatalogResourceType
    {
        [EnumMember(Value = "GOOGLE_DRIVE_FILE")]
        GoogleDriveItem,
        [EnumMember(Value = "GOOGLE_DRIVE_FOLDER")]
        MicrosoftDriveItem,
        [EnumMember(Value = "ZENDESK_TICKET")]
        ZendeskTicket,
        [EnumMember(Value = "ZENDESK_ARTICLE")]
        ZendeskArticle,
        [EnumMember(Value = "ZENDESK_GROUP")]
        ZendeskGroup,
        [EnumMember(Value = "ZENDESK_ARTICLE_SECTION")]
        ZendeskArticleSection,
        [EnumMember(Value = "ZENDESK_ARTICLE_CATEGORY")]
        ZendeskArticleCategory,
        [EnumMember(Value = "CONFLUENCE_PAGE")]
        ConfluencePage,
        [EnumMember(Value = "CONFLUENCE_SPACE")]
        ConfluenceSpace,
        [EnumMember(Value = "JIRA_TICKET")]
        JiraTicket,
        [EnumMember(Value = "JIRA_PROJECT")]
        JiraProject,
        [EnumMember(Value = "SALESFORCE_TASK")]
        SalesforceTask,
        [EnumMember(Value = "BOX_FILE")]
        BoxFile,
        [EnumMember(Value = "BOX_FOLDER")]
        BoxFolder,
        [EnumMember(Value = "NOTION_PAGE")]
        NotaionPage,
        [EnumMember(Value = "NOTION_DATABASE")]
        NotionDatabase,
        [EnumMember(Value = "SLACK_CHANNEL")]
        SlackChannel,
        [EnumMember(Value = "MONGO_COLECTION_SYNC")]
        MongoCollectionSync,
        [EnumMember(Value = "UNKNOWN")]
        Unknown
    }
}
