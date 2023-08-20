using Game.Forum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Forum.Persistence.Mappings
{
    public class VoteMapping : IEntityMapping<Vote>
    {
        public override void ConfigureDerivedEntityMapping(EntityTypeBuilder<Vote> builder)
        {
            builder.HasOne(d => d.Question)
                .WithMany(p => p.Votes)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vote_Question");

            builder.HasOne(d => d.User)
                .WithMany(p => p.Votes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vote_User");

            builder.ToTable("Vote");
        }
    }
}
