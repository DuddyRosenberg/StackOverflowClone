using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackOverflowClone.Data
{
    public class Repository
    {
        private string _connectionString;
        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Question> GetQuestions()
        {
            using (var context = new StackOverflowContext(_connectionString))
            {
                return context.Questions.Include(q => q.LikedQuestions).
                    Include(q => q.QuestionsTags).ThenInclude(qt => qt.Tag).OrderByDescending(o => o.DatePosted).ToList();
            }
        }
        public Question GetQuestion(int id)
        {
            using (var context = new StackOverflowContext(_connectionString))
            {
                return context.Questions.Include(q => q.LikedQuestions).Include(q => q.QuestionsTags).ThenInclude(qt => qt.Tag)
                    .FirstOrDefault(q => q.ID == id);
            }
        }
        public List<Answer> GetAnswers(int id)
        {
            using (var context = new StackOverflowContext(_connectionString))
            {
                return context.Answers.Include(a => a.User).Include(a => a.LikedAnswers)
                    .Where(a => a.QuestionID == id).ToList();
            }
        }
        public void AddQuestion(Question question, List<string> tags)
        {
            using (var context = new StackOverflowContext(_connectionString))
            {
                context.Questions.Add(question);
                foreach (var tag in tags)
                {
                    var tagFromDB = context.Tags.FirstOrDefault(t => t.Text == tag);
                    int tagID;
                    if (tagFromDB == null)
                    {
                        var newTag = new Tag { Text = tag };
                        context.Tags.Add(newTag);
                        context.SaveChanges();
                        tagID = newTag.ID;
                    }
                    else
                    {
                        context.SaveChanges();
                        tagID = tagFromDB.ID;
                    }
                    context.QuestionsTags.Add(new QuestionsTags
                    {
                        QuestionID = question.ID,
                        TagID = tagID
                    }); ;
                }
                context.SaveChanges();
            }
        }
        public void LikeQuestion(LikedQuestions like)
        {
            using (var context = new StackOverflowContext(_connectionString))
            {
                context.LikedQuestions.Add(like);
                context.SaveChanges();
            }
        }
        public void LikeAnswer(LikedAnswers like)
        {
            using (var context = new StackOverflowContext(_connectionString))
            {
                context.LikedAnswers.Add(like);
                context.SaveChanges();
            }
        }
        public void AddAnswer(Answer answer)
        {
            using (var context = new StackOverflowContext(_connectionString))
            {
                context.Answers.Add(answer);
                context.SaveChanges();
            }
        }
        public int GetUserID(string email)
        {
            using (var context = new StackOverflowContext(_connectionString))
            {
                var user = context.Users.FirstOrDefault(u => u.Email == email);
                return user == null ? 0 : user.ID;
            }
        }
        public void AddUser(User user, string password)
        {
            using (var context = new StackOverflowContext(_connectionString))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
        public bool Login(string email, string password)
        {
            using (var context = new StackOverflowContext(_connectionString))
            {
                if (context.Users.FirstOrDefault(u => u.Email == email) == null)
                { return false; }
                return BCrypt.Net.BCrypt.Verify(password, context.Users
                    .FirstOrDefault(u => u.Email == email).PasswordHash ?? "");
            }
        }
    }
}