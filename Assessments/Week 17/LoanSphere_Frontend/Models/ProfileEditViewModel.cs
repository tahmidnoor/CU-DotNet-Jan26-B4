using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace LoanSphere_Frontend.Models
{
    public class ProfileEditViewModel
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; }  // stored path
        public IFormFile? ProfileImage { get; set; }   // uploaded file

        [Display(Name = "PAN number")]
        public string? PanCardNumber { get; set; }

        [Display(Name = "Aadhaar number")]
        public string? AadhaarNumber { get; set; }

        public int CibilScore { get; set; }
        public bool IsProfileComplete { get; set; }
    }
}
