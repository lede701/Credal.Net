// Developed by: Leland Ede
// Created: 2025-01-22
// Updated: 2025-01-22
// Source: https://github.com/lede701/Credal.Net

namespace Credal.Net.Models.DocumentCatalog
{
    public class UploadDocumentContentsModel
    {
        public string DocumentName { get; set; }
        public string DocumentContents { get; set; }
        public ICollection<string> AllowedUsersEmailAddresses { get; set; }
        public string UploadAsrEmail { get; set; }
        public string DocumentExternalId { get; set; }
        public string? DocumentExternalUrl { get; set; }
        public string? CustomMetadata { get; set; }
        public string? CollectionId { get; set; }
        public bool? ForceUpdate { get; set; }
        public bool? InternalPublic { get; set; }

        public UploadDocumentContentsModel(string documentName, string documentContents, ICollection<string> allowedUsersEmailAddresses, string uploadAsrEmail, string documentExternalId)
        {
            this.DocumentName = documentName;
            this.DocumentContents = documentContents;
            this.AllowedUsersEmailAddresses = allowedUsersEmailAddresses;
            this.UploadAsrEmail = uploadAsrEmail;
            this.DocumentExternalId = documentExternalId;
            this.DocumentExternalUrl = null;
            this.CustomMetadata = null;
            this.CollectionId = null;
            this.ForceUpdate = null;
            this.InternalPublic = null;
        }
    }
}
