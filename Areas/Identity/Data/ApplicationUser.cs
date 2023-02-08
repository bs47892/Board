using Microsoft.AspNetCore.Identity;
using Board.Models;

namespace Board.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public virtual ICollection<List> Lists { get; set; }
}

public class ApplicationRole : IdentityRole
{

}
