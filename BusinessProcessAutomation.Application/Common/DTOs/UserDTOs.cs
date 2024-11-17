using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace BusinessProcessAutomation.Application.Common.DTOs
{
    public class AddOrUpdateUserDTO
    {
        [Required(AllowEmptyStrings = false)]
        public required string FirstName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public required string LastName { get; set; }
        public string? PhoneNumber { get; set; } = string.Empty;
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage ="Invalid email address.")]
        public required string Email { get; set; }
        public string? TimeInterval { get; set; } = string.Empty;
        public string? LinkedInUrl { get; set; } = string.Empty;
        public string? GitHubUrl { get; set; } = string.Empty;
        [Required(AllowEmptyStrings =false)]
        public required string Comment { get; set; }
    }

    public class UserDTO : AddOrUpdateUserDTO
    {
        public int Id { get; set; }
    }
}
