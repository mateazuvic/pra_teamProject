using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kwizzotronic.Models;

namespace Kwizzotronic.Controllers
{
    public class CreatorGameController : Controller
    {
        // GET: CreatorGame
        public ActionResult Index()
        {
            Random generator = new Random();
            string code = generator.Next(0, 1000000).ToString("D6");
            ViewBag.Code = code;
            ViewBag.NumberOfPlayers = "0";
            QuizStateMachine.CurrentQuizInstance = Repository.SaveQuizInstance(new QuizInstance((int)QuizStateMachine.Quiz.IDQuiz, code));
            return View();
        }

        public ActionResult Abort()
        {
            QuizStateMachine.ResetMachine();
            return RedirectToAction("Index", "User");
        }

        public ActionResult PlayQuiz()
        {
            QuizStateMachine.CurrentQuizInstance.HasStarted = 1;
            Repository.UpdateQuizInstance(QuizStateMachine.CurrentQuizInstance);
            QuizStateMachine.GetQuestionsForCurrentQuiz();
            Question question = QuizStateMachine.GetCurrentQuestion();
            ViewBag.Question = question.Text;
            ViewBag.Answer1 = question.Answer1;
            ViewBag.Answer2 = question.Answer2;
            ViewBag.Answer3 = question.Answer3;
            ViewBag.Answer4 = question.Answer4;
            ViewBag.QuestionNumber = QuizStateMachine.CurrentQuestionIndex.ToString();
            ViewBag.Time = question.Time.ToString();
            QuizStateMachine.TimerValue = question.Time;
            return View();
        }

        public ActionResult NextQuestion()
        {
            QuizStateMachine.CurrentQuestionIndex++;
            QuizStateMachine.GetQuestionsForCurrentQuiz();
            Question question = QuizStateMachine.GetCurrentQuestion();
            if (question == null)
            {
                QuizStateMachine.CurrentQuestionIndex--;
                return RedirectToAction("QuizDone");
            }
            else
            {
                return RedirectToAction("PlayQuiz");
            }

        }

        public ActionResult FinishQuiz()
        {
            QuizStateMachine.ResetMachine();
            return RedirectToAction("Index", "User");

        }

        public ActionResult ShowCorrectAnswer()
        {
            Question question = QuizStateMachine.GetCurrentQuestion();
            if (question == null)
            {
                return RedirectToAction("Index", "User");
            }
            ViewBag.Question = question.Text;
            switch (question.CorrectAnswer)
            {
                case 1:
                    ViewBag.CorrectAnswer = question.Answer1;
                    ViewBag.Class = "image-circle";
                    break;
                case 2:
                    ViewBag.CorrectAnswer = question.Answer2;
                    ViewBag.Class = "image-square";
                    break;
                case 3:
                    ViewBag.CorrectAnswer = question.Answer3;
                    ViewBag.Class = "image-triangle";
                    break;
                case 4:
                    ViewBag.CorrectAnswer = question.Answer4;
                    ViewBag.Class = "image-trapezoid";
                    break;
            }

            ViewBag.QuestionNumber = QuizStateMachine.CurrentQuestionIndex.ToString();
            ViewBag.Time = "0";
            return View();
        }

        public ActionResult QuizDone()
        {
            List<RankListEntry> rankList = Repository.GetRankListForQuiz(QuizStateMachine.CurrentQuizInstance.IDQuizInstance ?? 0).OrderByDescending(o => o.Points).ToList();
            ViewBag.First = rankList[0].Nickname;
            if (rankList.Count >= 2) ViewBag.Second = rankList[1].Nickname;
            if (rankList.Count >= 3) ViewBag.Second = rankList[2].Nickname;
            QuizStateMachine.CurrentQuizInstance.HasStarted = 3;
            return View();
        }

        [OutputCache(Duration = 0)]
        public ActionResult UpdateNumberOfPlayers()
        {

            ViewBag.NumberOfPlayers = QuizStateMachine.CurrentQuizInstance.NumberOfPlayers.ToString();
            return PartialView("NumberOfPlayersView");
        }

        [OutputCache(Duration = 0)]
        public ActionResult UpdateTime()
        {

            if (QuizStateMachine.TimerValue == 0)
            {
                QuizStateMachine.GetQuestionsForCurrentQuiz();
                return RedirectToAction("ShowCorrectAnswer");

            }
            else
            {
                QuizStateMachine.TimerValue = QuizStateMachine.TimerValue - 1;
                ViewBag.Question = QuizStateMachine.GetCurrentQuestion().Text;
                ViewBag.Answer1 = QuizStateMachine.GetCurrentQuestion().Answer1;
                ViewBag.Answer2 = QuizStateMachine.GetCurrentQuestion().Answer2;
                ViewBag.Answer3 = QuizStateMachine.GetCurrentQuestion().Answer3;
                ViewBag.Answer4 = QuizStateMachine.GetCurrentQuestion().Answer4;
                ViewBag.QuestionNumber = QuizStateMachine.CurrentQuestionIndex.ToString();
                ViewBag.Time = QuizStateMachine.TimerValue.ToString();
            }

            return PartialView("TimeView");
        }

    }
}