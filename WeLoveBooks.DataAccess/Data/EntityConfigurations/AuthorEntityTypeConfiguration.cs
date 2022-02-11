using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeLoveBooks.DataAccess.Models;

namespace WeLoveBooks.DataAccess.Data.EntityConfigurations;

internal class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder
            .HasOne(a => a.Photo)
            .WithOne(p => p.PhotoRelation as Author)
            .HasForeignKey(typeof(Photo), "Id")
            .IsRequired(false);

        builder
            .HasMany(a => a.Books)
            .WithOne(b => b.Author);
    }
}
