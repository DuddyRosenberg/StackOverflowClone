using System;
using System.Collections.Generic;
using System.Text;

namespace StackOverflowClone.Data
{
    public class LikedAnswers
    {
        public int AnswerID { get; set; }
        public int UserID { get; set; }
        public Answer Answer { get; set; }
        public User User { get; set; }
    }
}
