using Domain.Enums;

namespace Domain.Entities
{
    public class SystemLog
    {
        public ResourceType ResourceType { get; set; }
        public DateTime CreatedAt { get; set; }
        public Event Event { get; set; }
        public object[]? ChangeSet { get; set; }
        public string? Comment { get; set; }
    }
}
