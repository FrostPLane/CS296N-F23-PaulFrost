using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public string Title { get; set; }

}