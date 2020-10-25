using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataGrokrA2.Models
{
    public partial class LibraryContext : DbContext
    {
        public LibraryContext()
        {
        }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=LibraryDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasIndex(e => e.Abbrv)
                    .HasName("Abbrv");

                entity.HasIndex(e => e.FirstName)
                    .HasName("FirstName");

                entity.HasIndex(e => e.LastName)
                    .HasName("LastName");

                entity.Property(e => e.AuthorId)
                    .HasColumnName("AuthorID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Abbrv)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.Dod)
                    .HasColumnName("DOD")
                    .HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Authors)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_Authors_Books");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.BookId)
                    .HasName("BookID");

                entity.HasIndex(e => e.BookTitle)
                    .HasName("BookTitle");

                entity.HasIndex(e => e.TotalPages)
                    .HasName("TotalPages");

                entity.Property(e => e.BookId)
                    .HasColumnName("BookID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Abbrv)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AuthorId).HasColumnName("AuthorID");

                entity.Property(e => e.BookTitle)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.EditionNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .HasColumnName("ISBN")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Books_Authors");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
