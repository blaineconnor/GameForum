using Game.Forum.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Forum.Persistence.Mappings
{
    public abstract class BaseEntityMapping<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public abstract void ConfigureDerivedEntityMapping(EntityTypeBuilder<T> builder);

        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);

            //Intercepter
            ConfigureDerivedEntityMapping(builder);


            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("ID")
                .HasColumnOrder(1);

            ConfigureDerivedEntityMapping(builder);

            builder.Property(x => x.CreateDate)
                .HasColumnName("CREATE_DATE")
                .HasColumnOrder(26);

            builder.Property(x => x.CreatedBy)
                .HasColumnName("CREATED_BY")
                .HasColumnType("nvarchar(10)")
                .IsRequired(false)
                .HasColumnOrder(27);

            builder.Property(x => x.UpdatedDate)
                .HasColumnName("MODIFIED_DATE")
                .HasColumnOrder(28);

            builder.Property(x => x.UpdatedBy)
                .HasColumnName("MODIFIED_BY")
                .HasColumnType("nvarchar(10)")
                .IsRequired(false)
                .HasColumnOrder(29);

            builder.Property(x => x.IsDeleted)
                .HasColumnName("IS_DELETED")
                .HasDefaultValueSql("0")
                .HasColumnOrder(30);
        }
    }
}
