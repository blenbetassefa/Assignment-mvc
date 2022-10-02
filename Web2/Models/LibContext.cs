using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Web2.Models.NonDatabaseModels;

#nullable disable

namespace Web2.Models
{
    public partial class LibContext : DbContext
    {
        public LibContext()
        {
        }

        public LibContext(DbContextOptions<LibContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-DAHLJ73\\SQLEXPRESS; Initial Catalog=LibraryManagement;Integrated Security=True;Pooling=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasIndex(e => e.UserName, "UQ__Accounts__C9F28456767850CC")
                    .IsUnique();

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.AccountPassword)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.AccountType)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasIndex(e => e.AdminUserName, "UQ__Admins__B82E56ACAF4CB21C")
                    .IsUnique();

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.AdminDob)
                    .HasColumnType("date")
                    .HasColumnName("AdminDOB");

                entity.Property(e => e.AdminEmail)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.AdminGender)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AdminJoinedDate).HasColumnType("date");

                entity.Property(e => e.AdminName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.AdminPhone)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.AdminUserName)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.BookTblId)
                    .HasName("PK__Books__A155D6FDB8FA6D1E");

                entity.HasIndex(e => e.BookId, "UQ__Books__3DE0C226E335FF90")
                    .IsUnique();

                entity.Property(e => e.BookTblId).HasColumnName("BookTblID");

                entity.Property(e => e.BookAuthor)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.BookCategory)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BookCopyright)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.BookDateAdded).HasColumnType("date");

                entity.Property(e => e.BookId)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("BookID");

                entity.Property(e => e.BookImg).IsUnicode(false);

                entity.Property(e => e.BookIsbn)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("BookISBN");

                entity.Property(e => e.BookPublisher)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.BookStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BookTitle)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasIndex(e => e.MemberUserName, "UQ__Members__BD2BD5331C143C10")
                    .IsUnique();

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.MemberDob)
                    .HasColumnType("date")
                    .HasColumnName("MemberDOB");

                entity.Property(e => e.MemberEmail)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.MemberGender)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MemberJoinedDate).HasColumnType("date");

                entity.Property(e => e.MemberName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.MemberPhone)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MemberUserName)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.TranId)
                    .HasName("PK__Transact__F708962933D54FEC");

                entity.Property(e => e.TranId).HasColumnName("TranID");

                entity.Property(e => e.BookId)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("BookID");

                entity.Property(e => e.BookIsbn)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("BookISBN");

                entity.Property(e => e.BookTitle)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.MemberName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.TranDate).HasColumnType("date");

                entity.Property(e => e.TranStatus)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<Web2.Models.NonDatabaseModels.NewAdmin> NewAdmin { get; set; }

        public DbSet<Web2.Models.NonDatabaseModels.NewMember> NewMember { get; set; }
    }
}
