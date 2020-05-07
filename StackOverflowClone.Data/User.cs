using System;
using System.Collections.Generic;

namespace StackOverflowClone.Data
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public List<LikedQuestions> LikedQuestions { get; set; }
        public List<LikedAnswers> LikedAnswers { get; set; }
    }
}
