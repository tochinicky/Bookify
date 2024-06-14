using Bookify.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(user => user.Id);



        builder.Property(firstname => firstname.FirstName)
        .HasMaxLength(200)
        .HasConversion(firstname => firstname.Value, value => new FirstName(value));

        builder.Property(lastname => lastname.LastName)
         .HasMaxLength(200)
         .HasConversion(lastname => lastname.Value, value => new LastName(value));

        builder.Property(email => email.Email)
                .HasMaxLength(400)
                .HasConversion(email => email.Value, value => new Email(value));

        builder.HasIndex(user => user.Email).IsUnique();
    }
}
