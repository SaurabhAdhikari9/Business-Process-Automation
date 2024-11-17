using System.ComponentModel.DataAnnotations;

namespace BusinessProcessAutomation.Domain.Entities
{
    public class ApplicationUser : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [StringLength(maximumLength:15, ErrorMessage ="Invalid phone number")]
        public string? PhoneNumber { get; set; } = string.Empty;
        public required string Email { get; set; }
        public string? LinkedInUrl { get; set; } = string.Empty;
        public string? GitHubUrl { get; set; } = string.Empty;
        public required string Comment { get; set; }
        public string? TimeInterval { get; set; } = string.Empty;
    }
}
