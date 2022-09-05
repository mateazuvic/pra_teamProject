using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Kwizzotronic.Models
{
    public class Repository
    {
        public static Creator creator = null;
        private static string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public static Creator SaveCreator(Creator creator)
        {

            SqlParameter[] spParameter = new SqlParameter[5];

            spParameter[0] = new SqlParameter("@FirstName", SqlDbType.Text);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = creator.FirstName;

            spParameter[1] = new SqlParameter("@LastName", SqlDbType.Text);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = creator.LastName;


            spParameter[2] = new SqlParameter("@Email", SqlDbType.Text);
            spParameter[2].Direction = ParameterDirection.Input;
            spParameter[2].Value = creator.Email;

            spParameter[3] = new SqlParameter("@Password", SqlDbType.Text);
            spParameter[3].Direction = ParameterDirection.Input;
            spParameter[3].Value = creator.Password;

            spParameter[4] = new SqlParameter("@Id", SqlDbType.Int);
            spParameter[4].Direction = ParameterDirection.Output;

            try
            {
                SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "createCreator", spParameter);
                creator.IDCreator = Convert.ToInt32(spParameter[4].Value);

                return creator;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static Creator GetCreatorWithId(int id)
        {
            var tblCreators = SqlHelper.ExecuteDataset(connectionString, "getCreatorWithId", id);
            if (tblCreators.Tables.Count < 1 || tblCreators.Tables[0].Rows.Count < 1)
            {
                return null;
            }
            var dataRow = tblCreators.Tables[0].Rows[0];
            return new Creator(
                (int)dataRow["IDCreator"],
                dataRow["FirstName"].ToString(),
                dataRow["LastName"].ToString(),
                dataRow["Email"].ToString(),
                dataRow["Password"].ToString()
            );
        }

        public static Creator GetCreatorWithEmailAndPassword(string email, string password)
        {
            var tblCreators = SqlHelper.ExecuteDataset(connectionString, "getCreatorWithEmailAndPassword", email, password);
            if (tblCreators.Tables.Count < 1 || tblCreators.Tables[0].Rows.Count < 1)
            {
                return null;
            }
            var dataRow = tblCreators.Tables[0].Rows[0];
            return new Creator(
                (int)dataRow["IDCreator"],
                dataRow["FirstName"].ToString(),
                dataRow["LastName"].ToString(),
                dataRow["Email"].ToString(),
                dataRow["Password"].ToString()
            );
        }

        public static int UpdateCreator(Creator creator)
        {
            return SqlHelper.ExecuteNonQuery(connectionString, "UpdateCreator", creator.IDCreator,
                creator.FirstName, creator.LastName, creator.Email, creator.Password);
        }

        public static Quiz SaveQuiz(Quiz quiz)
        {

            SqlParameter[] spParameter = new SqlParameter[5];

            spParameter[0] = new SqlParameter("@Title", SqlDbType.Text);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = quiz.Title;


            spParameter[2] = new SqlParameter("@CreatorId", SqlDbType.Int);
            spParameter[2].Direction = ParameterDirection.Input;
            spParameter[2].Value = quiz.CreatorID;

            spParameter[3] = new SqlParameter("@Id", SqlDbType.Int);
            spParameter[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "createQuiz", spParameter);
            quiz.IDQuiz = Convert.ToInt32(spParameter[3].Value);

            return quiz;
        }

        public static int UpdateQuiz(Quiz quiz)
        {
            return SqlHelper.ExecuteNonQuery(connectionString, "UpdateQuiz", quiz.IDQuiz,
                quiz.Title, quiz.CreatorID);
        }

        public static List<Quiz> GetQuizesForCreator(int creatorId)
        {
            var tblQuizes = SqlHelper.ExecuteDataset(connectionString, "getQuizForUserWithId", creatorId);
            if (tblQuizes.Tables.Count < 1 || tblQuizes.Tables[0].Rows.Count < 1)
            {
                return new List<Quiz>();
            }
            List<Quiz> quizes = new List<Quiz>();

            foreach (DataRow row in tblQuizes.Tables[0].Rows)
            {
                quizes.Add(new Quiz(
                    idQuiz: (int)row["IDQuiz"],
                    title: row["Title"].ToString(),
                    creatorid: (int)row["CreatorId"]
               ));
            }
            return quizes;

        }

        public static List<Quiz> GetAllQuizes()
        {
            var tblQuizes = SqlHelper.ExecuteDataset(connectionString, "GetAllQuizes");
            if (tblQuizes.Tables.Count < 1 || tblQuizes.Tables[0].Rows.Count < 1)
            {
                return new List<Quiz>();
            }
            List<Quiz> quizes = new List<Quiz>();

            foreach (DataRow row in tblQuizes.Tables[0].Rows)
            {
                quizes.Add(new Quiz(
                    idQuiz: (int)row["IDQuiz"],
                    title: row["Title"].ToString(),
                    creatorid: (int)row["CreatorId"]
               ));
            }
            return quizes;

        }

        public static QuizInstance SaveQuizInstance(QuizInstance quizInstance)
        {

            SqlParameter[] spParameter = new SqlParameter[5];

            spParameter[0] = new SqlParameter("@QuizID", SqlDbType.Int);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = quizInstance.QuizID;

            spParameter[1] = new SqlParameter("@Code", SqlDbType.Text);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = quizInstance.Code;

            spParameter[2] = new SqlParameter("@NumberOfPlayers", SqlDbType.Int);
            spParameter[2].Direction = ParameterDirection.Input;
            spParameter[2].Value = 0;

            spParameter[3] = new SqlParameter("@HasStarted", SqlDbType.Int);
            spParameter[3].Direction = ParameterDirection.Input;
            spParameter[3].Value = 0;

            spParameter[4] = new SqlParameter("@Id", SqlDbType.Int);
            spParameter[4].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "createQuizInstance", spParameter);
            quizInstance.IDQuizInstance = Convert.ToInt32(spParameter[2].Value);

            return quizInstance;
        }

        public static int UpdateQuizInstance(QuizInstance quizInstance)
        {
            return SqlHelper.ExecuteNonQuery(connectionString, "updateQuizInstance", quizInstance.IDQuizInstance,
                quizInstance.Code, quizInstance.NumberOfPlayers, quizInstance.HasStarted);
        }

        public static QuizInstance GetQuizInstancesById(int quizInstanceId)
        {
            var tblCreators = SqlHelper.ExecuteDataset(connectionString, "getQuizInstanceById", quizInstanceId);
            if (tblCreators.Tables.Count < 1 || tblCreators.Tables[0].Rows.Count < 1)
            {
                return null;
            }
            var dataRow = tblCreators.Tables[0].Rows[0];
            return new QuizInstance(
                    idQuizInstance: (int)dataRow["IDQuizInstance"],
                    createdAt: dataRow["CreatedAt"].ToString(),
                    code: dataRow["Code"].ToString(),
                    numberOfPlayers: (int)dataRow["NumberOfPlayers"],
                    hasStarted: (int)dataRow["HasStarted"],
                    quizId: (int)dataRow["QuizId"]
               );
        }

        public static QuizInstance GetQuizInstancesByCode(string code)
        {
            var tblCreators = SqlHelper.ExecuteDataset(connectionString, "getQuizInstanceByCode", code);
            if (tblCreators.Tables.Count < 1 || tblCreators.Tables[0].Rows.Count < 1)
            {
                return null;
            }
            var dataRow = tblCreators.Tables[0].Rows[0];
            return new QuizInstance(
                    idQuizInstance: (int)dataRow["IDQuizInstance"],
                    createdAt: dataRow["CreatedAt"].ToString(),
                    code: dataRow["Code"].ToString(),
                    numberOfPlayers: (int)dataRow["NumberOfPlayers"],
                    hasStarted: (int)dataRow["HasStarted"],
                    quizId: (int)dataRow["QuizId"]
               );
        }

        public static List<QuizInstance> GetQuizInstancesForQuiz(int quizId)
        {
            var tblQuizInstances = SqlHelper.ExecuteDataset(connectionString, "getQuizInstanceWithQuizId", quizId);
            if (tblQuizInstances.Tables.Count < 1 || tblQuizInstances.Tables[0].Rows.Count < 1)
            {
                return null;
            }
            List<QuizInstance> quizInstances = new List<QuizInstance>();

            foreach (DataRow row in tblQuizInstances.Tables[0].Rows)
            {
                quizInstances.Add(new QuizInstance(
                    idQuizInstance: (int)row["IDQuizInstance"],
                    createdAt: row["CreatedAt"].ToString(),
                    code: row["Code"].ToString(),
                    numberOfPlayers: (int)row["NumberOfPlayers"],
                    hasStarted: (int)row["HasStarted"],
                    quizId: (int)row["QuizId"]
               ));
            }
            return quizInstances;

        }

        public static Nickname SaveNickname(Nickname nickname)
        {

            SqlParameter[] spParameter = new SqlParameter[5];

            spParameter[0] = new SqlParameter("@Text", SqlDbType.Text);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = nickname.Text;

            spParameter[1] = new SqlParameter("@QuizInstanceID", SqlDbType.Int);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = nickname.QuizInstanceID;

            spParameter[2] = new SqlParameter("@Id", SqlDbType.Int);
            spParameter[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "createNickname", spParameter);
            nickname.IDNickname = Convert.ToInt32(spParameter[1].Value);

            return nickname;
        }

        public static Boolean IsNicknameTaken(string nickname, int quizInstanceId)
        {
            var tblNickname = SqlHelper.ExecuteDataset(connectionString, "getNicknameWithQuizInstanceId", nickname, quizInstanceId);
            if (tblNickname.Tables.Count < 1 || tblNickname.Tables[0].Rows.Count < 1)
            {
                return false;
            }
            return true;

        }

        public static Question SaveQuestion(Question question)
        {

            SqlParameter[] spParameter = new SqlParameter[9];

            spParameter[0] = new SqlParameter("@Text", SqlDbType.Text);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = question.Text;

            spParameter[1] = new SqlParameter("@Answer1", SqlDbType.Text);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = question.Answer1;

            spParameter[2] = new SqlParameter("@Answer2", SqlDbType.Text);
            spParameter[2].Direction = ParameterDirection.Input;
            spParameter[2].Value = question.Answer2;

            spParameter[3] = new SqlParameter("@Answer3", SqlDbType.Text);
            spParameter[3].Direction = ParameterDirection.Input;
            spParameter[3].Value = question.Answer3;

            spParameter[4] = new SqlParameter("@Answer4", SqlDbType.Text);
            spParameter[4].Direction = ParameterDirection.Input;
            spParameter[4].Value = question.Answer4;

            spParameter[5] = new SqlParameter("@CorrectAnswer", SqlDbType.Int);
            spParameter[5].Direction = ParameterDirection.Input;
            spParameter[5].Value = question.CorrectAnswer;

            spParameter[6] = new SqlParameter("@Time", SqlDbType.Int);
            spParameter[6].Direction = ParameterDirection.Input;
            spParameter[6].Value = question.Time;

            spParameter[7] = new SqlParameter("@QuizId", SqlDbType.Int);
            spParameter[7].Direction = ParameterDirection.Input;
            spParameter[7].Value = question.QuizId;

            spParameter[8] = new SqlParameter("@Id", SqlDbType.Int);
            spParameter[8].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "createQuestion", spParameter);
            question.IDQuestion = Convert.ToInt32(spParameter[8].Value);

            return question;
        }

        public static int UpdateQuestion(Question question)
        {
            return SqlHelper.ExecuteNonQuery(connectionString, "UpdateQuestion", question.IDQuestion,
                question.Text, question.Answer1, question.Answer2, question.Answer3, question.Answer4,
                question.CorrectAnswer, question.Time, question.QuizId);
        }

        public static List<Question> GetQuestionsForQuiz(int quizId)
        {
            var tblQuestions = SqlHelper.ExecuteDataset(connectionString, "getQuestionsForQuiz", quizId);
            if (tblQuestions.Tables.Count < 1 || tblQuestions.Tables[0].Rows.Count < 1)
            {
                return new List<Question>();
            }
            List<Question> questions = new List<Question>();

            foreach (DataRow row in tblQuestions.Tables[0].Rows)
            {
                questions.Add(new Question(
                    idQuestion: (int)row["IDQuestion"],
                    text: row["Text"].ToString(),
                    answer1: row["Answer1"].ToString(),
                    answer2: row["Answer2"].ToString(),
                    answer3: row["Answer3"].ToString(),
                    answer4: row["Answer4"].ToString(),
                    correctAnswer: (int)row["CorrectAnswer"],
                    time: (int)row["Time"],
                    quizId: (int)row["QuizId"]
               ));
            }
            return questions;

        }

        public static RankListEntry SaveToRankList(RankListEntry rangListEntry)
        {

            SqlParameter[] spParameter = new SqlParameter[5];

            spParameter[0] = new SqlParameter("@Position", SqlDbType.Int);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = rangListEntry.Position;

            spParameter[1] = new SqlParameter("@Points", SqlDbType.Int);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = rangListEntry.Points;

            spParameter[2] = new SqlParameter("@Nickname", SqlDbType.Text);
            spParameter[2].Direction = ParameterDirection.Input;
            spParameter[2].Value = rangListEntry.Nickname;

            spParameter[3] = new SqlParameter("@QuizInstanceId", SqlDbType.Int);
            spParameter[3].Direction = ParameterDirection.Input;
            spParameter[3].Value = rangListEntry.QuizInstanceId;


            spParameter[4] = new SqlParameter("@Id", SqlDbType.Int);
            spParameter[4].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "createRankList", spParameter);
            rangListEntry.IDRankList = Convert.ToInt32(spParameter[4].Value);

            return rangListEntry;
        }

        public static int UpdateRankListEntry(RankListEntry rankListEntry)
        {
            return SqlHelper.ExecuteNonQuery(connectionString, "UpdateRankList", rankListEntry.IDRankList,
                rankListEntry.Position, rankListEntry.Points, rankListEntry.Nickname, rankListEntry.QuizInstanceId);
        }

        public static int GetRankListPosition(RankListEntry rankListEntry)
        {
            List<RankListEntry> rankList = GetRankListForQuiz(rankListEntry.QuizInstanceId).OrderByDescending(o => o.Points).ToList();
            for (int i = 0; i < rankList.Count; i++)
            {
                if (rankList[i].IDRankList == rankListEntry.IDRankList)
                {
                    return i + 1;
                }
            }
            return 0;
        }

        public static List<RankListEntry> GetRankListForQuiz(int quizInstanceId)
        {
            var tblRankList = SqlHelper.ExecuteDataset(connectionString, "getRankListForQuizInstance", quizInstanceId);
            if (tblRankList.Tables.Count < 1 || tblRankList.Tables[0].Rows.Count < 1)
            {
                return null;
            }
            List<RankListEntry> ranks = new List<RankListEntry>();

            foreach (DataRow row in tblRankList.Tables[0].Rows)
            {
                ranks.Add(new RankListEntry(
                    idRankList: (int)row["IDRankList"],
                    position: (int)row["Position"],
                    points: (int)row["Points"],
                    nickname: row["Nickname"].ToString(),
                    quizInstanceId: (int)row["QuizInstanceId"]
               ));
            }
            return ranks;

        }

    }
}