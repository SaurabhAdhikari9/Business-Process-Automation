using Microsoft.AspNetCore.Identity;

namespace BusinessProcessAutomation.Domain.Entities
{
    public class ApplicationUser : IdentityUser<long>
    {
        public string? LinkedInUrl { get; set; }
        public string? GitHubUrl { get; set; }
        public string? Comment { get; set; }
        public string? TimeInterval { get; set; }
    }
}
