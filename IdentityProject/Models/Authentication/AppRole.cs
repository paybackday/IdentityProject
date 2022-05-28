using Microsoft.AspNetCore.Identity;
using System;

namespace IdentityProject.Models.Authentication
{
    public class AppRole:IdentityRole
    {
        public DateTime CreatedDate { get; set; }
    }
}
