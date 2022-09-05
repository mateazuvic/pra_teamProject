using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kwizzotronic.Models;

namespace Kwizzotronic.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Index()
        {
            ViewBag.creator = Repository.creator;
            return View();
        }

        public ActionResult EditProfile()
        {
            ViewBag.creator = Repository.creator;
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(string FirstName, string LastName)
        {

            var rows = Repository.UpdateCreator(new Creator(idCreator: Repository.creator.IDCreator ?? 0, firstName: FirstName, lastName: LastName,
                email: Repository.creator.Email, password: Repository.creator.Password));

            if (rows > 0)
            {
                Repository.creator.FirstName = FirstName;
                Repository.creator.LastName = LastName;
            }

            ViewBag.creator = Repository.creator;

            return View();

        }

        public ActionResult SignOut()
        {
            Repository.creator = null;
            return RedirectToAction("Index", "Auth");
        }

        public ActionResult EditPassword()
        {
            ViewBag.creator = Repository.creator;
            return View();
        }

        [HttpPost]
        public ActionResult EditPassword(string OldPassword, string NewPassword, string ConfirmNewPassword)
        {

            if (OldPassword != Repository.creator.Password)
            {
                ViewBag.creator = Repository.creator;
                ViewBag.ErrorMessage = Resources.Web_sitemap.incorrectOldPassword;
                return View("EditProfile");
            }
            if (NewPassword != ConfirmNewPassword)
            {
                ViewBag.creator = Repository.creator;
                ViewBag.ErrorMessage = Resources.Web_sitemap.confirmPasswordDontMatch;
                return View("EditProfile");
            }

            var rows = Repository.UpdateCreator(new Creator(idCreator: Repository.creator.IDCreator ?? 0,
                firstName: Repository.creator.FirstName, lastName: Repository.creator.LastName,
                email: Repository.creator.Email, password: ConfirmNewPassword));

            if (rows > 0)
            {
                Repository.creator.Password = ConfirmNewPassword;
            }

            ViewBag.creator = Repository.creator;

            return View("EditProfile");

        }

        public ActionResult MyQuizes()
        {
            var quizes = Repository.GetQuizesForCreator(Repository.creator.IDCreator ?? 0);
            return View(quizes);
        }

        public ActionResult StartQuiz(int? quizId)
        {
            Quiz quiz = GetQuiz(quizId ?? 0);
            QuizStateMachine.Quiz = quiz;
            return RedirectToAction("Index", "CreatorGame");
        }

        public ActionResult EditQuiz(int? quizId)
        {
            Quiz quiz = GetQuiz(quizId ?? 0);
            List<Question> questions = Repository.GetQuestionsForQuiz(quiz.IDQuiz ?? -1);
            QuizStateMachine.Quiz = quiz;
            QuizStateMachine.Questions = questions;
            QuizStateMachine.QuizTitle = quiz.Title;

            return RedirectToAction("Index", "QuizDetails");
        }

        public ActionResult GetRankingList(int? quizId)
        {
            Quiz quiz = GetQuiz(quizId ?? 0);
            if (quiz.IDQuiz != null)
            {
                var quizInstances = Repository.GetQuizInstancesForQuiz((int)quiz.IDQuiz);
                if (quizInstances != null)
                {
                    QuizStateMachine.QuizInstances = quizInstances;
                    return RedirectToAction("Index", "RankingList");
                }
            }


            return View();
        }

        private Quiz GetQuiz(int id)
        {
            Quiz quiz = null;
            var quizes = Repository.GetQuizesForCreator(Repository.creator.IDCreator ?? 0);
            foreach (var item in quizes)
            {
                if (item.IDQuiz == id)
                {
                    quiz = item;
                }
            }
            return quiz;
        }

    }

}