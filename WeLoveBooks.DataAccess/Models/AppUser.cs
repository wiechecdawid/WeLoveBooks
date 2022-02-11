using Microsoft.AspNetCore.Identity;

namespace WeLoveBooks.DataAccess.Models;

public class AppUser: IdentityUser, IPhotoRelation
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhotoId { get; set; }
    public virtual Photo? Photo { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<Comment> Comments { get; set; }
}
