using System.ComponentModel.DataAnnotations;

namespace IdentityProject.VMClasses
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Please fill up email address.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter your email address with correct format.")]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please fill up your password.")]
        [DataType(DataType.Password, ErrorMessage = "Please enter your password with correct format.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        /// <summary>
        /// Remember me?
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool Persistent { get; set; }
        public bool Lock { get; set; }
    }
}
