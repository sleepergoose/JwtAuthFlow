using Domain.Constants;
using Domain.Entities;
using Domain.ValueObjects.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EFCore.Config;

internal sealed class WriteConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var nameConverter = new ValueConverter<UserName, string>(
            valueToDatabase => valueToDatabase.Value,
            valueFromDatabase => new UserName(valueFromDatabase)
            );

        var emailConverter = new ValueConverter<UserEmail, string>(
            valueToDB => valueToDB.Value,
            valueFromDB => new UserEmail(valueFromDB)
            );

        var passwordHashConverter = new ValueConverter<UserPasswordHash, string>(
            valueToDB => valueToDB.Value,
            valueFromDB => new UserPasswordHash(valueFromDB)
            );

        var roleConverter = new ValueConverter<Role, int>(
            valueToDB => (int)valueToDB,
            valueFromDB => valueFromDB == 1 ? Role.Admin : Role.User
            );

        builder.ToTable(Constants.UsersTableName);

        builder.HasKey(x => x.Id);

        builder
            .Property(p => p.Id)
            .HasConversion(id => id.Value, id => new UserId(id));

        builder
            .Property(typeof(UserName), "_name")
            .HasConversion(nameConverter)
            .HasColumnName(Constants.UsersTableColumnNames[nameof(UserName)]);

        builder
            .Property<UserEmail>("_email")
            .HasConversion(emailConverter)
            .HasColumnName(Constants.UsersTableColumnNames[nameof(UserEmail)]);

        builder
            .Property<UserPasswordHash>("_passwordHash")
            .HasConversion(passwordHashConverter)
            .HasColumnName(Constants.UsersTableColumnNames[nameof(UserPasswordHash)]);

        builder
            .Property<Role>("_role")
            .HasConversion(roleConverter)
            .HasColumnName(Constants.UsersTableColumnNames[nameof(Role)]);
    }
}
