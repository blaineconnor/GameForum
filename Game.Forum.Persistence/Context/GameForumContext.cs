using Game.Forum.Domain.Common;
using Game.Forum.Domain.Entities;
using Game.Forum.Domain.Services.Abstraction;
using Game.Forum.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Game.Forum.Persistence.Context
{
    public partial class GameForumContext : DbContext
    {
        private readonly ILoggedUserService _loggedUserService;

        public GameForumContext(DbContextOptions<GameForumContext> options, ILoggedUserService loggedUserService) : base(options)
        {
            _loggedUserService = loggedUserService;
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Answer> Answers { get; set; } = null!;
        public virtual DbSet<Favorite> Favorites { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<QuestionView> QuestionViews { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Vote> Votes { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfiguration(new AccountMapping());
            modelBuilder.ApplyConfiguration(new AnswerMapping());
            modelBuilder.ApplyConfiguration(new CategoryMapping());
            modelBuilder.ApplyConfiguration(new FavoriteMapping());
            modelBuilder.ApplyConfiguration(new QuestionMapping());
            modelBuilder.ApplyConfiguration(new QuestionViewMapper());
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new VoteMapping());

            modelBuilder.Entity<Account>().HasQueryFilter(e => e.IsDeleted == null || (e.IsDeleted.HasValue && !e.IsDeleted.Value));
            modelBuilder.Entity<Question>().HasQueryFilter(e => e.IsDeleted == null || (e.IsDeleted.HasValue && !e.IsDeleted.Value));
            modelBuilder.Entity<User>().HasQueryFilter(e => e.IsDeleted == null || (e.IsDeleted.HasValue && !e.IsDeleted.Value));
            modelBuilder.Entity<Answer>().HasQueryFilter(e => e.IsDeleted == null || (e.IsDeleted.HasValue && !e.IsDeleted.Value));
            modelBuilder.Entity<Favorite>().HasQueryFilter(e => !e.IsDeleted == null || (e.IsDeleted.HasValue && !e.IsDeleted.Value));
            modelBuilder.Entity<Category>().HasQueryFilter(e => e.IsDeleted == null || (e.IsDeleted.HasValue && !e.IsDeleted.Value));
            modelBuilder.Entity<QuestionView>().HasQueryFilter(e => e.IsDeleted == null || (e.IsDeleted.HasValue && !e.IsDeleted.Value));
            modelBuilder.Entity<Vote>().HasQueryFilter(e => e.IsDeleted == null || (e.IsDeleted.HasValue && !e.IsDeleted.Value));
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            //Herhangi bir kayıt işleminde yapılan işlem ekleme ise CreateDate ve CreatedBy bilgileri otomatik olarak set edilir.
            //Herhangi bir kayıt işleminde yapılan işlem güncelleme ise ModifiedDate ve ModifiedBy bilgileri otomatik olarak set edilir.

            var entries = ChangeTracker.Entries<BaseEntity>().ToList();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.IsDeleted = true;
                    entry.State = EntityState.Modified;
                }

                if (entry.Entity is AuditableEntity auditableEntity)
                {
                    switch (entry.State)
                    {
                        //update
                        case EntityState.Modified:
                            auditableEntity.ModifiedDate = DateTime.Now;
                            auditableEntity.ModifiedBy = _loggedUserService.Username ?? "admin";
                            break;
                        //insert
                        case EntityState.Added:
                            auditableEntity.CreateDate = DateTime.Now;
                            auditableEntity.CreatedBy = _loggedUserService.Username ?? "admin";
                            break;
                        //delete
                        case EntityState.Deleted:
                            auditableEntity.ModifiedDate = DateTime.Now;
                            auditableEntity.ModifiedBy = _loggedUserService.Username ?? "admin";
                            break;
                        default:
                            break;
                    }
                }

            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


    }
}
