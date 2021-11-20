using Microsoft.AspNetCore.Identity;

namespace WeLoveBooks.DataAccess.Models;

public class AppUser: IdentityUser
{
    public ICollection<Review> Reviews { get; set; }
    public ICollection<Comment> Comments { get; set; }
}
