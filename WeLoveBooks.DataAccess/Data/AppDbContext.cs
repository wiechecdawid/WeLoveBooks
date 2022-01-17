﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeLoveBooks.DataAccess.Data.EntityConfigurations;
using WeLoveBooks.DataAccess.Models;

namespace WeLoveBooks.DataAccess.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext() { }
    public AppDbContext(DbContextOptions options): base(options)
    {    }

    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<BookRate> BookRates { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);

        builder.Entity<Comment>()
            .HasOne(c => c.AppUser)
            .WithMany(au => au.Comments)
            .HasForeignKey(c => c.AppUserId)
            .IsRequired();

        new ReviewEntityTypeConfiguration().Configure(builder.Entity<Review>());
        // modelBuilder.ApplyConfiguration(new ReviewEntityTypeConfiguration())
    }
}
