using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackOverflowClone.Data
{
    public class StackOverflowContext : DbContext
    {
        private readonly string _connectionString;
        public StackOverflowContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<LikedQuestions> LikedQuestions { get; set; }
        public DbSet<LikedAnswers> LikedAnswers { get; set; }
        public DbSet<QuestionsTags> QuestionsTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<LikedQuestions>()
                .HasKey(lq => new { lq.QuestionID, lq.UserID });

            modelBuilder.Entity<LikedQuestions>()
                .HasOne(lq => lq.Question)
                .WithMany(q => q.LikedQuestions)
                .HasForeignKey(lq => lq.QuestionID);

            modelBuilder.Entity<LikedQuestions>()
                .HasOne(lq => lq.User)
                .WithMany(u => u.LikedQuestions)
                .HasForeignKey(lq => lq.UserID);


            modelBuilder.Entity<LikedAnswers>()
                .HasKey(la => new { la.AnswerID, la.UserID });

            modelBuilder.Entity<LikedAnswers>()
                .HasOne(la => la.Answer)
                .WithMany(a => a.LikedAnswers)
                .HasForeignKey(la => la.AnswerID);

            modelBuilder.Entity<LikedAnswers>()
                .HasOne(la => la.User)
                .WithMany(u => u.LikedAnswers)
                .HasForeignKey(la => la.UserID);


            modelBuilder.Entity<QuestionsTags>()
                .HasKey(qt => new { qt.QuestionID, qt.TagID });

            modelBuilder.Entity<QuestionsTags>()
                .HasOne(qt => qt.Question)
                .WithMany(q => q.QuestionsTags)
                .HasForeignKey(qt => qt.QuestionID);

            modelBuilder.Entity<QuestionsTags>()
                .HasOne(qt => qt.Tag)
                .WithMany(t => t.QuestionsTags)
                .HasForeignKey(qt => qt.TagID);
        }
    }
}
