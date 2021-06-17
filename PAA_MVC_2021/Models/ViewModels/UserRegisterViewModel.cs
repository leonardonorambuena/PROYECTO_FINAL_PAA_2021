using System.ComponentModel.DataAnnotations;

namespace PAA_MVC_2021.Models.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required]
        [MaxLength(100)]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(120)]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }
        [Required]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name = "Confirme Contraseña")]
        public string PasswordConfirm { get; set; }
    }
}