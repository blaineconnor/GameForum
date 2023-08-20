using Game.Forum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Forum.Persistence.Mappings
{
    public class AnswerMapping : IEntityMapping<Answer>
    {
        public override void ConfigureDerivedEntityMapping(EntityTypeBuilder<Answer> builder)
        {
            builder.Property(e => e.Content)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.CreatedTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.UpdatedTime).HasColumnType("datetime");

            builder.HasOne(d => d.Question)
                .WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Answer_Question");

            builder.HasOne(d => d.User)
                .WithMany(p => p.Answers)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Answer_User");

            builder.ToTable("ANSWERS");
        }
    }
}
