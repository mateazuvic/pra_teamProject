using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kwizzotronic.Models
{
    public class Quiz
    {
        public int? IDQuiz { get; set; }
        public string Title { get; set; }
        public int CreatorID { get; set; }

        public Quiz(string title, int creatorid)
        {
            this.Title = title;
            this.CreatorID = creatorid;
        }

        public Quiz(int idQuiz, string title, int creatorid)
        {
            this.IDQuiz = idQuiz;
            this.Title = title;
            this.CreatorID = creatorid;
        }
    }
}