using Game.Forum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Forum.Persistence.Mappings
{
    public class QuestionMapping : IEntityMapping<Question>
    {
        public override void ConfigureDerivedEntityMapping(EntityTypeBuilder<Question> builder)
        {
            builder.Property(e => e.Category)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            builder.Property(e => e.Content)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Title)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.UpdatedTime).HasColumnType("datetime");

            builder.HasOne(d => d.User)
                .WithMany(p => p.Questions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Question_User");

            builder.ToTable("QUESTIONS");
        }
    }
}
