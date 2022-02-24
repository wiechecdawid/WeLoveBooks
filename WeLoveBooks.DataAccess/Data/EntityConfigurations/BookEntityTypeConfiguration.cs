using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeLoveBooks.DataAccess.Models;

namespace WeLoveBooks.DataAccess.Data.EntityConfigurations;

internal class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .IsRequired();

        //builder
        //    .HasOne(b => b.Photo)
        //    .WithOne(p => p.PhotoRelation as Book)
        //    .HasForeignKey<Photo>(p => p.Id)
        //    .IsRequired(false);
    }
}
