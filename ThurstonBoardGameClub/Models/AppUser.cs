using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

public class AppUser : IdentityUser
{
    public string? Name { get; set; }
    public string? Title { get; set; }

    [NotMapped]
    public IList<string> RoleNames { get; set; } = null!;

}