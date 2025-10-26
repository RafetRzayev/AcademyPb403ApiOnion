
using Microsoft.AspNetCore.Identity;

namespace Academy.Domain.Entities;

public class AppUser : IdentityUser
{
    public string? FullName {  get; set; }
}
