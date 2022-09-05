using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kwizzotronic.Models
{
    public class Question
    {
        public int? IDQuestion { get; set; }
        public string Text { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public String Answer3 { get; set; }
        public String Answer4 { get; set; }
        public int CorrectAnswer { get; set; }
        public int Time { get; set; }
        public int QuizId { get; set; }

        public Question(int idQuestion, string text, string answer1, string answer2, String answer3, String answer4, int correctAnswer, int time, int quizId)
        {
            this.IDQuestion = idQuestion;
            this.Text = text;
            this.Answer1 = answer1;
            this.Answer2 = answer2;
            this.Answer3 = answer3;
            this.Answer4 = answer4;
            this.CorrectAnswer = correctAnswer;
            this.Time = time;
            this.QuizId = quizId;
        }

        public Question(string text, string answer1, string answer2, String answer3, String answer4, int correctAnswer, int time, int quizId)
        {
            this.Text = text;
            this.Answer1 = answer1;
            this.Answer2 = answer2;
            this.Answer3 = answer3;
            this.Answer4 = answer4;
            this.CorrectAnswer = correctAnswer;
            this.Time = time;
            this.QuizId = quizId;
        }

    }

}