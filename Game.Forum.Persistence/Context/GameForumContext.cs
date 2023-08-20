using Game.Forum.Application.Services.Abstraction;
using Game.Forum.Domain.Common;
using Game.Forum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Game.Forum.Persistence.Context
{
    public partial class GameForumContext : DbContext
    {
        private readonly IUserService _userService;
        public GameForumContext()
        {
        }

        public GameForumContext(DbContextOptions<GameForumContext> options, IUserService userService) : base(options)
        {
            _userService = userService;
        }

        public virtual DbSet<Answer> Answers { get; set; } = null!;
        public virtual DbSet<Favorite> Favorites { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<QuestionView> QuestionViews { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Vote> Votes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=IKYSDB;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Question>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<User>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Answer>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Favorite>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(e => !e.IsDeleted);
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

                if (entry.Entity is BaseEntity baseEntity)
                {
                    switch (entry.State)
                    {
                        //update
                        case EntityState.Modified:
                            baseEntity.UpdatedDate = DateTime.Now;
                            baseEntity.UpdatedBy = _userService.Username ?? "admin";
                            break;
                        //insert
                        case EntityState.Added:
                            baseEntity.CreateDate = DateTime.Now;
                            baseEntity.CreatedBy = _userService.Username ?? "admin";
                            break;
                        //delete
                        case EntityState.Deleted:
                            baseEntity.UpdatedDate = DateTime.Now;
                            baseEntity.UpdatedBy = _userService.Username ?? "admin";
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
