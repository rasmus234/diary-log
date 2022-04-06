#nullable enable
using Microsoft.EntityFrameworkCore;

namespace DiaryLogDomain;

public partial class DiaryLogContext : DbContext
{
    public DiaryLogContext()
    {
    }

    public DiaryLogContext(DbContextOptions<DiaryLogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; } = null!;
    public virtual DbSet<Comment> Comments { get; set; } = null!;
    public virtual DbSet<Post> Posts { get; set; } = null!;
    public virtual DbSet<PostCategory> PostCategories { get; set; } = null!;
    public virtual DbSet<Rating> Ratings { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DiaryLogConnectionString"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(30)
                .HasColumnName("category_name");

            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Categories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Category_User");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comment");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Content)
                .HasMaxLength(300)
                .HasColumnName("content");

            entity.Property(e => e.PostId).HasColumnName("post_id");

            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_Post");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Comment_User");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.ToTable("Post");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Content)
                .HasMaxLength(3600)
                .HasColumnName("content");

            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");

            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");

            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Post_User");
        });

        modelBuilder.Entity<PostCategory>(entity =>
        {
            entity.HasKey(e => new { e.CategoryId, e.PostId });

            entity.ToTable("PostCategory");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");

            entity.Property(e => e.PostId).HasColumnName("post_id");

            entity.HasOne(d => d.Category)
                .WithMany(p => p.PostCategories)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_PostCategory_Category");

            entity.HasOne(d => d.Post)
                .WithMany(p => p.PostCategories)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostCategory_Post");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.PostId })
                .HasName("PK__Rating__CA534F790EB06E18");

            entity.ToTable("Rating");

            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.Property(e => e.PostId).HasColumnName("post_id");

            entity.Property(e => e.IsLike).HasColumnName("is_like");

            entity.HasOne(d => d.Post)
                .WithMany(p => p.Ratings)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rating_Post");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Ratings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Rating_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .HasColumnName("name");

            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .HasColumnName("password");

            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .HasColumnName("username");

            entity.HasIndex(e => e.Username)
                .IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}