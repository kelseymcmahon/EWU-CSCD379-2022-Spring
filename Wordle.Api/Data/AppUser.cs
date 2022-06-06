using Microsoft.AspNetCore.Identity;

namespace Wordle.Api.Data
{
    public class AppUser : IdentityUser //TODO: Spin an ef migration for Dob
    {
        public DateTime Dob { get; set; }
    }
}