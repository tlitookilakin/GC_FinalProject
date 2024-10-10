using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectBackend.Models;

public partial class FinalProjectDbContext : DbContext
{
    private readonly IConfiguration configuration;
    public FinalProjectDbContext()
    {
    }

    public FinalProjectDbContext(DbContextOptions<FinalProjectDbContext> options, IConfiguration config)
        : base(options)
    {
        this.configuration = config;
        Console.WriteLine(config["connection"]);
    }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer(configuration["connection"]);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Recipes__3214EC07D753BF75");

            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Recipes__UserId__5EBF139D");
        });

        modelBuilder.Entity<RecipeIngredient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RecipeIn__3214EC07678D9D7D");

            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Unit).HasMaxLength(16);

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeIngredients)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__RecipeIng__Recip__619B8048");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0722E884E8");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
