using Microsoft.AspNetCore.Identity;

namespace IdentityProject.Models.Authentication
{
    public class AppUser:IdentityUser
    {
        public string Memleket { get; set; }
        public bool Cinsiyet { get; set; }
    }
}
