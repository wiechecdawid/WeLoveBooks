﻿using Microsoft.AspNetCore.Identity;

namespace WeLoveBooks.DataAccess.Models;

public class AppUser: IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<BookRate> BookRates { get; set; }
}
