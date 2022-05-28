using IdentityProject.Models.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityProject.Models
{
    public class AppDbContext:IdentityDbContext<AppUser,AppRole,string>
    {
        //Yukarida verdigimiz string degeri veritabaninda bulunan primary key kolonunun tipini belirler.
        public AppDbContext(DbContextOptions<AppDbContext> dbContext) : base(dbContext) 
        { 
        
        }
    }
}
