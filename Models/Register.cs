using System.ComponentModel.DataAnnotations;

namespace RecipeAZ.Models {
    public class Register {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
    }
}
