using Game.Forum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Forum.Persistence.Mappings
{
    public class UserMapping : AuditableEntityMapping<User>
    {
        public override void ConfigureDerivedEntityMapping(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnName("NAME")
                .HasColumnType("nvarchar(30)")
                .IsRequired()
                .HasColumnOrder(5);

            builder.Property(x => x.Surname)
                .HasColumnName("SURNAME")
                .HasColumnType("nvarchar(30)")
                .IsRequired()
                .HasColumnOrder(6);

            builder.Property(x => x.Email)
                .HasColumnName("EMAIL")
                .HasColumnType("nvarchar(150)")
                .IsRequired()
                .HasColumnOrder(7);


            builder.Property(x => x.Birthdate)
                .HasColumnName("BIRTHDATE")
                .IsRequired()
                .HasColumnOrder(9);

            builder.Property(x => x.IsDeleted)
                .HasColumnType("bit")
                .HasColumnName("IS_DELETED")
                .IsRequired(false);

            builder.Property(x => x.Gender)
                .HasColumnName("GENDER")
                .IsRequired()
                .HasColumnOrder(10);

            builder.Property(e => e.Image)
                .IsUnicode(false);

            builder.ToTable("USER");

        }
    }
}
