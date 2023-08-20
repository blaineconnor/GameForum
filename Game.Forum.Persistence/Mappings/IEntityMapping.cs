using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Forum.Persistence.Mappings
{
    public abstract class IEntityMapping<T> : IEntityTypeConfiguration<T> where T : class
    {
        public abstract void ConfigureDerivedEntityMapping(EntityTypeBuilder<T> builder);

        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Equals(typeof(T)));
           
        }
    }
}
