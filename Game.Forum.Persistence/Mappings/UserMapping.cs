using Game.Forum.Domain.Common;
using Game.Forum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Forum.Persistence.Mappings
{
    public class UserMapping : BaseEntityMapping<User>
    {
        public override void ConfigureDerivedEntityMapping(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id)
                .HasColumnName("USER_ID")
                .HasColumnOrder(2);

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasColumnType("nvarchar(10)")
                .HasColumnName("USER_NAME")
                .HasColumnOrder(3);

            builder.Property(x => x.Password)
              .IsRequired()
              .HasColumnType("nvarchar(100)")
              .HasColumnName("PASSWORD")
              .HasColumnOrder(4);

            builder.Property(x => x.LastLoginDate)
                .HasColumnName("LAST_LOGIN_DATE")
                .IsRequired(false)
                .HasColumnOrder(5);

            builder.Property(x => x.LastUserIp)
                .HasColumnType("nvarchar(50)")
                .HasColumnName("LAST_LOGIN_IP")
                .IsRequired(false)
                .HasColumnOrder(6);

            builder.Property(x => x.Role)
                .HasColumnName("ROLE_ID")
                .HasColumnOrder(7);

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Surname)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Birthdate)
                .HasColumnName("BIRTHDATE")
                .IsRequired()
                .HasColumnOrder(9);

            builder.Property(x => x.Votes)
                .IsRequired();

            builder.Property(x => x.Gender)
                .IsRequired();

            builder.Property(e => e.CreatedTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Email)
                .HasMaxLength(50);

            builder.Property(e => e.Image)
                .IsUnicode(false);

            builder.Property(e => e.Location)
                .HasMaxLength(50);

            builder.Property(e => e.UpdatedTime)
                .HasColumnType("datetime");

            builder.ToTable("USER");

        }
    }
}
