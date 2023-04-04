using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CodeParts.Data.TableModel;

namespace CodeParts.Data.EFConfiguration
{
    public class CodeConfig : IEntityTypeConfiguration<CodeDb>
    {
        public const string Table_name = "content";
        public void Configure(EntityTypeBuilder<CodeDb> builder)
        {
            builder.ToTable(Table_name);

            builder.HasKey(e => e.id);

            builder.Property(e => e.id)
                   .HasColumnType(EntityDataTypes.Bigint)
                   .HasColumnName("pk_code_id");

            builder.Property(e => e.accountLogin)
                   .HasColumnType(EntityDataTypes.Character_varying)
                   .HasColumnName("fk_account_login");

            builder.Property(e => e.title)
                   .HasColumnType(EntityDataTypes.Character_varying)
                   .HasColumnName("title");

            builder.Property(e => e.content)
                   .HasColumnType(EntityDataTypes.Character_varying)
                   .HasColumnName("content");

            builder.Property(e => e.tags)
                   .HasColumnType(EntityDataTypes.Character_varying)
                   .HasColumnName("tags");
        }
    }
}
