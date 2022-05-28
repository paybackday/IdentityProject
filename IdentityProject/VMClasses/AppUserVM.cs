using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IdentityProject.VMClasses
{
    public class AppUserVM
    {
        [Required(ErrorMessage ="Lutfen Kullanici adini bos gecmeyiniz.")]
        [StringLength(15,ErrorMessage ="Lutfen kullanici adini 4-15 karakter arasinda giriniz")]
        [Display(Name="Kullanici adi")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Lutfen email alanini bos gecmeyiniz")]
        [EmailAddress(ErrorMessage ="Lutfen email formatini dogru giriniz.")]
        [Display(Name="Email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Lutfen sifre alanini bos gecmeyiniz")]
        [DataType(DataType.Password,ErrorMessage ="Lutfen sifreyi tum kurallari goz ununde bulundurarak doldurunuz.")]
        [Display(Name ="Sifre")]
        public string Sifre { get; set; }
    }
}
