using Game.Forum.Domain.Entities;
using Game.Forum.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ahlatci.Shop.Persistence.Mappings
{
    public class CategoryMapping : BaseEntityMapping<Category>
    {
        public override void ConfigureDerivedEntityMapping(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.CategoryName)
                .IsRequired()
                .HasColumnType("nvarchar(100)")
                .HasColumnName("CATEGORY_NAME")
                .HasColumnOrder(2);

            builder.ToTable("CATEGORIES");
        }
    }
}
