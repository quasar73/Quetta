using Quetta.Common.Enums;

namespace Quetta.Common.Models.Responses
{
    public class ChatItemResponse
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public ChatType ChatType { get; set; }

        public string? LastMessage { get; set; }

        public string? LastMessageSecretVersion { get; set; }
    }
}
