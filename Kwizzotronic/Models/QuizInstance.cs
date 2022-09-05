using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kwizzotronic.Models
{
    public class QuizInstance
    {
        public int? IDQuizInstance { get; set; }
        public String CreatedAt { get; set; }

        public String Code { get; set; }

        public int NumberOfPlayers { get; set; }

        public int HasStarted { get; set; }
        public int QuizID { get; set; }

        public QuizInstance(int idQuizInstance, string createdAt, string code, int numberOfPlayers, int hasStarted, int quizId)
        {
            this.IDQuizInstance = idQuizInstance;
            this.CreatedAt = createdAt;
            this.Code = code;
            this.NumberOfPlayers = numberOfPlayers;
            this.HasStarted = hasStarted;
            this.QuizID = quizId;
        }

        public QuizInstance(int quizId, string code)
        {
            this.QuizID = quizId;
            this.Code = code;
            this.HasStarted = 0;
        }


    }

}