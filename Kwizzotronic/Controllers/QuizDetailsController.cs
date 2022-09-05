using Kwizzotronic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kwizzotronic.Controllers
{
    public class QuizDetailsController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Question = QuizStateMachine.GetFirstQuestion();
            ViewBag.QuizTitle = QuizStateMachine.QuizTitle;
            ViewBag.CurrentQuestion = QuizStateMachine.CurrentQuestionIndex.ToString();
            return View();
        }

        [HttpPost]
        public ActionResult NextQuestion(String quizTitle, String questionText, String answer1,
            String answer2, String answer3, String answer4, String time, String correctAnswer)
        {
            if (string.IsNullOrEmpty(answer3))
            {
                answer3 = null;
            }
            if (string.IsNullOrEmpty(answer4))
            {
                answer4 = null;
            }
            QuizStateMachine.QuizTitle = quizTitle;
            var question = QuizStateMachine.GetNextQuestion(new Question(
                text: questionText,
                answer1: answer1,
                answer2: answer2,
                answer3: answer3,
                answer4: answer4,
                time: Int32.Parse(time),
                correctAnswer: Int32.Parse(correctAnswer),
                quizId: -1));
            ModelState.Clear();
            ViewBag.Question = question;
            ViewBag.QuizTitle = quizTitle;
            ViewBag.CurrentQuestion = QuizStateMachine.CurrentQuestionIndex.ToString();
            return View("Index");
        }

        [HttpPost]
        public ActionResult PreviousQuestion(String quizTitle, String questionText, String answer1,
            String answer2, String answer3, String answer4, String time, String correctAnswer)
        {
            if (string.IsNullOrEmpty(answer3))
            {
                answer3 = null;
            }
            if (string.IsNullOrEmpty(answer4))
            {
                answer4 = null;
            }
            QuizStateMachine.QuizTitle = quizTitle;
            var question = QuizStateMachine.GetPreviousQuestion(new Question(
                text: questionText,
                answer1: answer1,
                answer2: answer2,
                answer3: answer3,
                answer4: answer4,
                time: Int32.Parse(time),
                correctAnswer: Int32.Parse(correctAnswer),
                quizId: -1));
            ModelState.Clear();
            ViewBag.Question = question;
            ViewBag.QuizTitle = quizTitle;
            ViewBag.CurrentQuestion = QuizStateMachine.CurrentQuestionIndex.ToString();
            return View("Index");
        }

        [HttpPost]
        public ActionResult SaveQuestion(String quizTitle, String questionText, String answer1,
            String answer2, String answer3, String answer4, String time, String correctAnswer)
        {
            if (string.IsNullOrEmpty(answer3))
            {
                answer3 = null;
            }
            if (string.IsNullOrEmpty(answer4))
            {
                answer4 = null;
            }
            QuizStateMachine.QuizTitle = quizTitle;
            var question = new Question(
                text: questionText,
                answer1: answer1,
                answer2: answer2,
                answer3: answer3,
                answer4: answer4,
                time: Int32.Parse(time),
                correctAnswer: Int32.Parse(correctAnswer),
                quizId: -1);
            QuizStateMachine.UpdateLastQuestion(question);
            QuizStateMachine.Save();
            return RedirectToAction("Index", "User");
        }

        public ActionResult Save()
        {
            QuizStateMachine.Save();
            return RedirectToAction("Index", "User");
        }

        public ActionResult Abort()
        {
            QuizStateMachine.ResetMachine();
            return RedirectToAction("Index", "User");
        }

    }
}