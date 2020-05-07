using StackOverflowClone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflowClone.Web.Models
{
    public class QuestionPageViewModel
    {
        public int UserID { get; set; }
        public Question Question { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
