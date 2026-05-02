namespace LoanSphere_Frontend.Models
{
    public class AuthenticatedUser
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

        // 🔥 ADD THIS
        public string? ProfilePictureUrl { get; set; }
    }
}