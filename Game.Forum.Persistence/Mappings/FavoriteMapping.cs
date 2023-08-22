using Game.Forum.Domain.Common;
using Game.Forum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Forum.Persistence.Mappings
{
    public class FavoriteMapping : BaseEntityMapping<Favorite>
    {
        public override void ConfigureDerivedEntityMapping(EntityTypeBuilder<Favorite> builder)
        {
            builder.Property(e => e.CreatedTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.Question)
                .WithMany(p => p.Favorites)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Favorite_Question");

            builder.HasOne(d => d.User)
                .WithMany(p => p.Favorites)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Favorite_User");

            builder.ToTable("FAVORITE");
        }
    }
}
