using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kwizzotronic.Models
{
    public class RankListEntry
    {
        public int? IDRankList { get; set; }
        public int Position { get; set; }
        public int Points { get; set; }
        public string Nickname { get; set; }
        public int QuizInstanceId { get; set; }

        public RankListEntry(int idRankList, int position, int points, string nickname, int quizInstanceId)
        {
            this.IDRankList = idRankList;
            this.Position = position;
            this.Points = points;
            this.Nickname = nickname;
            this.QuizInstanceId = quizInstanceId;
        }

        public RankListEntry(int position, int points, string nickname, int quizInstanceId)
        {
            this.Position = position;
            this.Points = points;
            this.Nickname = nickname;
            this.QuizInstanceId = quizInstanceId;
        }

    }

}