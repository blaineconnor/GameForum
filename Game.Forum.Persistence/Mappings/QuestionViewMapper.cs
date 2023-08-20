using Game.Forum.Domain.Common;
using Game.Forum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Forum.Persistence.Mappings
{
    public class QuestionViewMapper : IEntityMapping<QuestionView>
    {
        public override void ConfigureDerivedEntityMapping(EntityTypeBuilder<QuestionView> builder)
        {
            builder.Property(e => e.CreatedTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.Question)
                .WithMany(p => p.QuestionViews)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionView_Question");

            builder.HasOne(d => d.User)
                .WithMany(p => p.QuestionViews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionView_User");

            builder.ToTable("QuestionView");
        }
    }
}
