using System;
using System.Collections.Generic;
using MembukuAPI.Authors;
using MembukuAPI.Books;
using MembukuAPI.HighlightedBooks;
using MembukuAPI.Reviews;
using MembukuAPI.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MembukuAPI.Data
{
    public partial class MembukuContext : DbContext
    {
        public MembukuContext()
        {
        }

        public MembukuContext(DbContextOptions<MembukuContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<HighlightedBook> HighlightedBooks { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-RP150ADH;Initial Catalog=Membuku;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Cover)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK__Books__AuthorId__2A4B4B5E");
            });

            modelBuilder.Entity<HighlightedBook>(entity =>
            {
                entity.HasKey(e => e.BookId)
                    .HasName("PK__Highligh__3DE0C2074D26F23D");

                entity.Property(e => e.BookId).ValueGeneratedNever();

                entity.Property(e => e.AddedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Book)
                    .WithOne(p => p.HighlightedBook)
                    .HasForeignKey<HighlightedBook>(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Highlight__BookI__33D4B598");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.BookId })
                    .HasName("PK__Reviews__30B289C53B381E2D");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ReadDate).HasColumnType("date");

                entity.Property(e => e.ReadStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ReviewDate).HasColumnType("datetime");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reviews__BookId__2E1BDC42");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reviews__Usernam__2D27B809");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Users__536C85E5D4976895");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.JoinDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastActiveDate).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
