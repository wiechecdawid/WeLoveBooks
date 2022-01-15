using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeLoveBooks.DataAccess.Models;

namespace WeLoveBooks.DataAccess.Data.EntityConfigurations;

public class BookRateEntityTypeConfiguration : IEntityTypeConfiguration<BookRate>
{
    public void Configure(EntityTypeBuilder<BookRate> builder)
    {
        builder
            .HasOne(br => br.AppUser)
            .WithMany(au => au.BookRates)
            .HasForeignKey(br => br.AppUserId);

        builder
            .HasOne(br => br.Book)
            .WithMany(b => b.BookRates)
            .HasForeignKey(br => br.BookId);
    }
}
