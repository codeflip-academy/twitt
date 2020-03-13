using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TwittAPI.Models
{
    public partial class TwittContext : DbContext
    {
        public TwittContext()
        {
        }

        public TwittContext(DbContextOptions<TwittContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<CommentsCount> CommentsCount { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<Reaction> Reaction { get; set; }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Message)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.HasOne(d => d.MessageNavigation)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__Message__3E52440B");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__Profile__3D5E1FD2");
            });

            modelBuilder.Entity<CommentsCount>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CommentsCount");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Text)
                    .HasColumnName("Message")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Picture).HasMaxLength(1);

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Message__Profile__3A81B327");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                    .HasName("UQ__Profile__C9F28456E33DCF75")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reaction>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.MessageNavigation)
                    .WithMany(p => p.Reaction)
                    .HasForeignKey(d => d.Message)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reaction__Messag__4316F928");

                entity.HasOne(d => d.ProfileNavigation)
                    .WithMany(p => p.Reaction)
                    .HasForeignKey(d => d.Profile)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reaction__Profil__4222D4EF");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
