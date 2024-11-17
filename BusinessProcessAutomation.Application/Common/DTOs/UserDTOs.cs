namespace BusinessProcessAutomation.Application.Common.DTOs
{
    public class AddOrUpdateUserDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public required string Email { get; set; }
        public string TimeInterval { get; set; } = string.Empty;
        public string LinkedInUrl { get; set; } = string.Empty;
        public string GitHubUrl { get; set; } = string.Empty;
        public required string Comment { get; set; }
    }
}
