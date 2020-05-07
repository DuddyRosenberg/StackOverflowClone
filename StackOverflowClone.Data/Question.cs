using System;
using System.Collections.Generic;
using System.Text;

namespace StackOverflowClone.Data
{
    public class Question
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string QuestionText { get; set; }
        public DateTime DatePosted { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public List<LikedQuestions> LikedQuestions { get; set; }
        public List<QuestionsTags> QuestionsTags { get; set; }
    }
}
