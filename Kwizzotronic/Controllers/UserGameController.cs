using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kwizzotronic.Models;

namespace Kwizzotronic.Controllers
{
    public class UserGameController : Controller
    {
        // GET: UserGame

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Start(string nickname, string code)
        {
            QuizInstance instance = Repository.GetQuizInstancesByCode(code);
            if (instance != null)
            {
                if (!Repository.IsNicknameTaken(nickname, instance.IDQuizInstance ?? 0))
                {
                    instance.NumberOfPlayers++;
                    QuizStateMachine.CurrentQuizInstance = instance;
                    QuizStateMachine.Questions = Repository.GetQuestionsForQuiz(instance.QuizID);
                    Repository.UpdateQuizInstance(instance);

                    Nickname newNickname = new Nickname(nickname, instance.IDQuizInstance ?? 0);
                    Session["Nickname"] = Repository.SaveNickname(newNickname);

                    RankListEntry rankListEntry = new RankListEntry(0, 0, newNickname.Text, newNickname.QuizInstanceID);
                    Session["RankList"] = Repository.SaveToRankList(rankListEntry);

                    return View();
                }
                else
                {
                    ViewBag.ErrorMessage = Resources.Web_sitemap.nicknameTaken;
                    return View("Index");
                }

            }

            return View("Index");
        }

        public ActionResult Abort()
        {
            QuizStateMachine.CurrentQuizInstance.NumberOfPlayers--;
            Repository.UpdateQuizInstance(QuizStateMachine.CurrentQuizInstance);

            return RedirectToAction("SelectMode", "Start");
        }

        [HttpGet]
        public ActionResult Refresh()
        {
            QuizInstance instance = Repository.GetQuizInstancesById(QuizStateMachine.CurrentQuizInstance.IDQuizInstance ?? 0);
            if (instance != null && instance.HasStarted == 1)
            {
                return View("StartGame");
            }
            return View("Start");
        }

        public ActionResult StartGame()
        {

            return View();
        }

        [OutputCache(Duration = 0)]
        public ActionResult CheckState()
        {
            if (QuizStateMachine.CurrentQuizInstance != null && QuizStateMachine.CurrentQuizInstance.HasStarted == 3)
            {
                return RedirectToAction("ShowFinalScoreState");
            }

            if ((Session["QuestionAnswered"] as string) == QuizStateMachine.CurrentQuestionIndex.ToString())
            {

                return RedirectToAction("ShowCurrentState");
            }



            if (QuizStateMachine.TimerValue == 0)
            {
                Session["StatusMessage"] = "Incorrect answer!!";
                return RedirectToAction("ShowCurrentState");

            }
            else
            {

                ViewBag.Answer1 = QuizStateMachine.GetCurrentQuestion().Answer1;
                ViewBag.Answer2 = QuizStateMachine.GetCurrentQuestion().Answer2;
                ViewBag.Answer3 = QuizStateMachine.GetCurrentQuestion().Answer3;
                ViewBag.Answer4 = QuizStateMachine.GetCurrentQuestion().Answer4;
                ViewBag.QuestionNumber = QuizStateMachine.CurrentQuestionIndex.ToString();

                return PartialView("GameView");
            }


        }

        public ActionResult ShowCurrentState()
        {
            ViewBag.StatusMessage = Session["StatusMessage"] as string;
            ViewBag.Points = (Session["RankList"] as RankListEntry).Points.ToString();
            return View();
        }

        public ActionResult ShowFinalScoreState()
        {
            ViewBag.Position = Repository.GetRankListPosition(Session["RankList"] as RankListEntry).ToString();
            ViewBag.Nickname = (Session["Nickname"] as Nickname).Text;
            ViewBag.Points = (Session["RankList"] as RankListEntry).Points.ToString();
            return View();
        }

        [HttpGet]
        public ActionResult Answer1()
        {
            UpdatePoints(1);
            Session["QuestionAnswered"] = QuizStateMachine.CurrentQuestionIndex.ToString();
            return View("StartGame");
        }

        [HttpGet]
        public ActionResult Answer2()
        {
            UpdatePoints(2);
            Session["QuestionAnswered"] = QuizStateMachine.CurrentQuestionIndex.ToString();
            return View("StartGame");
        }

        [HttpGet]
        public ActionResult Answer3()
        {
            UpdatePoints(3);
            Session["QuestionAnswered"] = QuizStateMachine.CurrentQuestionIndex.ToString();
            return View("StartGame");
        }

        [HttpGet]
        public ActionResult Answer4()
        {
            UpdatePoints(4);
            Session["QuestionAnswered"] = QuizStateMachine.CurrentQuestionIndex.ToString();
            return View("StartGame");
        }

        private int CalculatePoints()
        {
            int questionTime = QuizStateMachine.GetCurrentQuestion().Time;
            int timeAnswered = QuizStateMachine.TimerValue;
            double time = (double)timeAnswered / questionTime;
            int result = (int)(time * 200.0);
            return result;
        }

        private void UpdatePoints(int answerNumber)
        {
            if (QuizStateMachine.GetCurrentQuestion().CorrectAnswer == answerNumber)
            {
                RankListEntry rankListEntry = Session["RankList"] as RankListEntry;
                int points = CalculatePoints();
                rankListEntry.Points += points;
                Session["RankList"] = rankListEntry;
                Repository.UpdateRankListEntry(rankListEntry);
                Session["StatusMessage"] = Resources.Web_sitemap.correctAnswer;
            }
            else
            {
                Session["StatusMessage"] = Resources.Web_sitemap.incorrectAnswer;
            }
        }

    }
}