using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kwizzotronic.Models
{
    public class Nickname
    {
        public int? IDNickname { get; set; }
        public String Text { get; set; }
        public int QuizInstanceID { get; set; }

        public Nickname(int idNickname, string text, int quizInstanceId)
        {
            this.IDNickname = idNickname;
            this.Text = text;
            this.QuizInstanceID = quizInstanceId;
        }

        public Nickname(string text, int quizInstanceId)
        {
            this.Text = text;
            this.QuizInstanceID = quizInstanceId;
        }

    }
}