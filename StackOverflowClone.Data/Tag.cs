using System;
using System.Collections.Generic;
using System.Text;

namespace StackOverflowClone.Data
{
    public class Tag
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public List<QuestionsTags> QuestionsTags { get; set; }
    }
}
