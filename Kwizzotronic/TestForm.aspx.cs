using Kwizzotronic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kwizzotronic
{
    public partial class TestForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDodaj_Click(object sender, EventArgs e)
        {
            /**
            var user = Repository.SaveCreator(
                new Creator(
                    firstName: txtIme.Text, 
                    lastName: txtPrezime.Text, 
                    email: txtEmail.Text, 
                    password: txtLozinka.Text)
                );
            txtNewId.Text = user.IDCreator.ToString();
            
            var creator = Repository.GetCreatorWithId(2);
            txtNewId.Text = "0";

            var user = Repository.GetCreatorWithEmailAndPassword(txtEmail.Text, txtLozinka.Text);
            if (user == null)
            {
                txtNewId.Text = "Wrong email or password";
            } else
            {
                txtNewId.Text = user.IDCreator.ToString();
            }

            var a = Repository.UpdateCreator(new Creator(idCreator: 1, ime: txtIme.Text, prezime: txtPrezime.Text, email: txtEmail.Text, lozinka: txtLozinka.Text));
            txtNewId.Text = a.ToString();

            var a2 = Repository.SaveQuiz(new Quiz(title: txtIme.Text, code: txtPrezime.Text, creatorid: 2));
            txtNewId.Text = a2.IDQuiz.ToString();

            var a3 = Repository.UpdateQuiz(new Quiz(idQuiz: 4, title: txtIme.Text, code: txtPrezime.Text, creatorid: 1));
            txtNewId.Text = a3.ToString(); 

            var quizes = Repository.GetQuizesForCreator(2);
            txtNewId.Text = quizes.Count.ToString();

            var question = Repository.SaveQuestion(
                new Question("ttt2", "a1", "a2", null, null, 1, 20, 2)
            );
             var question1 = Repository.SaveQuestion(
               new Question("ttt1", "a11", "a12", null, null, 1, 20, 2)
            );

            var question2 = Repository.SaveQuestion(
               new Question("ttt2", "a21", "a22", "a23", null, 3, 20, 2)
            );

            var question3 = Repository.SaveQuestion(
               new Question("ttt3", "a31", "a32", null, null, 2, 20, 1)
            );

            var question4 = Repository.SaveQuestion(
               new Question("ttt4", "a41", "a42", null, null, 1, 20, 2)
            );
            txtNewId.Text = question.IDQuestion.ToString();

            var a4 = Repository.UpdateQuestion(
                new Question(1, "ttt1New", "a1", "a2", null, null, 1, 20, 2)
            );
            txtNewId.Text = a4.ToString();

           
            var questions = Repository.GetQuestionsForQuiz(2);
            txtNewId.Text = questions.Count.ToString();

            var rankListEntry = Repository.SaveToRankList(new RankListEntry(
                position: 2, points: 100, nickname: "aaaa222", quizId: 1
            ));
            var rankListEntr2y = Repository.SaveToRankList(new RankListEntry(
                position: 1, points: 120, nickname: "cc2222c", quizId: 1
            ));
            var rankListEntry = Repository.SaveToRankList(new RankListEntry(
                position: 2, points: 100, nickname: "aaaa222", quizId: 1
            ));
            var rankListEntry2 = Repository.SaveToRankList(new RankListEntry(
                position: 1, points: 120, nickname: "cc2222c", quizId: 1
            ));

            var rankListEntry3 = Repository.SaveToRankList(new RankListEntry(
                position: 2, points: 100, nickname: "aaaa222", quizId: 2
            ));
            var rankListEntry4 = Repository.SaveToRankList(new RankListEntry(
                position: 1, points: 120, nickname: "cc2222c", quizId: 2
            ));
            var rankListEntry5 = Repository.SaveToRankList(new RankListEntry(
                position: 3, points: 90, nickname: "cc2222c", quizId: 2
            ));


            var quizInstance = Repository.GetQuizInstancesForQuiz(
                1
            );
            txtNewId.Text = quizInstance.Count.ToString();

             var isTaken = Repository.IsNicknameTaken("n3", 3);
            if (isTaken)
            {
                txtNewId.Text = "username taken";
            }
            else
            {
                txtNewId.Text = "username available";
            }

            var raknkListEnteries = Repository.GetRankListForQuiz(2);
            txtNewId.Text = raknkListEnteries.Count.ToString();*/




            var quizInstance = Repository.GetQuizInstancesForQuiz(
                1
            );
            txtNewId.Text = quizInstance.Count.ToString();

        }


    }
}