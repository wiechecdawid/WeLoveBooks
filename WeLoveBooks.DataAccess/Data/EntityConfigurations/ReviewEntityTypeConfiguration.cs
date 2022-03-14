using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeLoveBooks.DataAccess.Models;

namespace WeLoveBooks.DataAccess.Data.EntityConfigurations;

internal class ReviewEntityTypeConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder
            .HasOne(r => r.Book)
            .WithMany(b => b.Reviews)
            .HasForeignKey(r => r.BookId);

        builder
            .HasOne(r => r.AppUser)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.AppUserId)
            .IsRequired();

        builder
            .HasMany(r => r.Comments)
            .WithOne(c => c.Review)
            .HasForeignKey(r => r.ReviewId);

        builder
            .HasOne(r => r.BookRate)
            .WithOne(br => br.Review)
            .HasForeignKey<Review>();
    }
}