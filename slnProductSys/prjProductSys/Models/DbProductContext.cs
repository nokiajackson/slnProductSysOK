using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace prjProductSys.Models;

public partial class DbProductContext : DbContext
{
    public DbProductContext()
    {
    }

    public DbProductContext(DbContextOptions<DbProductContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TCategory> TCategories { get; set; }

    public virtual DbSet<TComment> TComments { get; set; }

    public virtual DbSet<TMember> TMembers { get; set; }

    public virtual DbSet<TProduct> TProducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\Desktop\\20230612OK\\slnProductSys\\prjProductSys\\App_Data\\dbProduct.mdf;Integrated Security=True;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__tCategor__19093A0B929D174A");

            entity.ToTable("tCategory");

            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<TComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tComment__3214EC07A4D9F854");

            entity.ToTable("tComment");

            entity.Property(e => e.Comment).HasColumnType("ntext");
            entity.Property(e => e.IsReComment)
                .HasMaxLength(50)
                .HasColumnName("isReComment");
            entity.Property(e => e.ProductId).HasMaxLength(50);
            entity.Property(e => e.PublishDate).HasColumnType("datetime");
            entity.Property(e => e.ReComment).HasColumnType("ntext");
        });

        modelBuilder.Entity<TMember>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("PK__tMember__C5B69A4A069882BA");

            entity.ToTable("tMember");

            entity.Property(e => e.Uid).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Pwd).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        modelBuilder.Entity<TProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__tProduct__B40CC6CDDD047FA9");

            entity.ToTable("tProduct");

            entity.Property(e => e.ProductId).HasMaxLength(50);
            entity.Property(e => e.Img).HasMaxLength(50);
            entity.Property(e => e.ProductName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
