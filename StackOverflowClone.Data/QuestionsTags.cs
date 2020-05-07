using System;
using System.Collections.Generic;
using System.Text;

namespace StackOverflowClone.Data
{
    public class QuestionsTags
    {
        public int QuestionID { get; set; }
        public int TagID { get; set; }
        public Question Question { get; set; }
        public Tag Tag { get; set; }
    }
}
