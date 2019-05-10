using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ThesisProject.Models
{
    public partial class ThesisProjectDBContext : DbContext
    {
        public ThesisProjectDBContext()
        {
        }

        public ThesisProjectDBContext(DbContextOptions<ThesisProjectDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<ExamFile> ExamFile { get; set; }
        public virtual DbSet<ExerciseFile> ExerciseFile { get; set; }
        public virtual DbSet<Facts> Facts { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Module> Module { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=ThesisProjectDB;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ExamFile>(entity =>
            {
                entity.Property(e => e.Extn)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Language).HasMaxLength(10);

                entity.Property(e => e.ModuleId).HasColumnName("Module_Id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.ExamFile)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK__ExamFile__Fk_Mod__3C69FB99");
            });

            modelBuilder.Entity<ExerciseFile>(entity =>
            {
                entity.Property(e => e.Extn)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Language).HasMaxLength(10);

                entity.Property(e => e.ModuleId).HasColumnName("Module_Id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.ExerciseFile)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK__ExerciseF__Fk_Mo__3F466844");
            });

            modelBuilder.Entity<Facts>(entity =>
            {
                entity.Property(e => e.Extn)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ModuleId).HasColumnName("Module_Id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Facts)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK__Facts__Module_Id__68487DD7");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.Extn)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Language).HasMaxLength(10);

                entity.Property(e => e.ModuleId).HasColumnName("Module_Id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Image)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK__Image__Fk_Module__44FF419A");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.Property(e => e.CourseId).HasColumnName("Course_Id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Module)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Module__Fk_Cours__398D8EEE");
            });
        }
    }
}
