using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CodeParts.Data.TableModel;

namespace CodeParts.Data.EFConfiguration
{
    public class AccountConfig : IEntityTypeConfiguration<AccountDb>
    {
        public const string Table_name = "accounts";
        public void Configure(EntityTypeBuilder<AccountDb> builder)
        {
            builder.ToTable(Table_name);

            builder.HasKey(e => new { e.login });
            builder.HasIndex(e => e.login);


            builder.Property(e => e.password)
                   .HasColumnType(EntityDataTypes.Character_varying)
                   .HasColumnName("password");


            builder.Property(e => e.nickname)
                   .HasColumnType(EntityDataTypes.Character_varying)
                   .HasColumnName("nickname");

            builder.Property(e => e.email)
                   .HasColumnType(EntityDataTypes.Character_varying)
                   .HasColumnName("email");

            builder.Property(e => e.role)
                   .HasColumnType(EntityDataTypes.Smallint)
                   .HasColumnName("role");

            builder.HasMany(d => d.Posts)
                   .WithOne(p => p.Account)
                   .HasPrincipalKey(p => p.login)
                   .HasForeignKey(d => d.accountLogin);
        }
    }
}
