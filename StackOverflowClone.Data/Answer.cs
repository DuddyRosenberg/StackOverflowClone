using System;
using System.Collections.Generic;
using System.Text;

namespace StackOverflowClone.Data
{
    public class Answer
    {
        public int ID { get; set; }
        public string AnswerText { get; set; }
        public int QuestionID { get; set; }
        public int UserID { get; set; }
        public Question Question { get; set; }
        public User User { get; set; }
        public List<LikedAnswers> LikedAnswers { get; set; }
    }
}
