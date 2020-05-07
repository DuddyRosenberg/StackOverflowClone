using System;
using System.Collections.Generic;
using System.Text;

namespace StackOverflowClone.Data
{
    public class LikedQuestions
    {
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        public User User { get; set; }
        public Question Question { get; set; }
    }
}
