using System.ComponentModel.DataAnnotations;

namespace TODO.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Név")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Jelszó")]
        public string Password { get; set; }

        [Display(Name = "Emlékezz rám?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Név")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} legalább {2} karakter hosszúnak kell lennie.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Jelszó")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Jelszó újra")]
        [Compare("Password", ErrorMessage = "A 2 jelszó nem egyezik.")]
        public string ConfirmPassword { get; set; }
    }
}
