using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace todolist_api1.Model
{
    public partial class dbContext : DbContext
    {
        public dbContext()
        {
        }

        public dbContext(DbContextOptions<dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Priority> Priorities { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserTask> UserTasks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=debian;Port=5432;Database=db;Username=dbuser;Password=1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Priority>(entity =>
            {
                entity.ToTable("priority", "todolist_schema");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user", "todolist_schema");

                entity.Property(e => e.UserId).UseIdentityAlwaysColumn();

                entity.Property(e => e.PasswordHash).HasMaxLength(256);

                entity.Property(e => e.Username).HasMaxLength(30);
            });

            modelBuilder.Entity<UserTask>(entity =>
            {
                entity.ToTable("user_task", "todolist_schema");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.PriorityNavigation)
                    .WithMany(p => p.UserTasks)
                    .HasForeignKey(d => d.Priority)
                    .HasConstraintName("fk_priority");

/*                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTasks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user");*/
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
