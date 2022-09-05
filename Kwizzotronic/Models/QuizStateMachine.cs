using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kwizzotronic.Models
{
    public class QuizStateMachine
    {
        public static Quiz Quiz = null;
        public static int CurrentQuestionIndex = 1;
        public static List<Question> Questions = new List<Question>();
        public static List<QuizInstance> QuizInstances = new List<QuizInstance>();
        public static String QuizTitle = null;
        public static QuizInstance CurrentQuizInstance = null;
        public static int TimerValue = 0;

        public static void ResetMachine()
        {
            Quiz = null;
            CurrentQuestionIndex = 1;
            Questions = new List<Question>();
            QuizTitle = null;
            CurrentQuizInstance = null;
            TimerValue = 0;
        }

        public static void GetQuestionsForCurrentQuiz()
        {
            if (Quiz != null)
            {
                Questions = Repository.GetQuestionsForQuiz(Quiz.IDQuiz ?? 0);
            }

        }

        public static Question GetCurrentQuestion()
        {
            if (Questions.Count == 0 || CurrentQuestionIndex <= 0 || CurrentQuestionIndex > Questions.Count) return null;
            return Questions[CurrentQuestionIndex - 1];
        }

        public static Question GetPreviousQuestion(Question currentQuestion)
        {
            if (Questions.Count > 0 && CurrentQuestionIndex >= 0 && CurrentQuestionIndex <= Questions.Count)
            {
                currentQuestion.IDQuestion = Questions[CurrentQuestionIndex - 1].IDQuestion;
                currentQuestion.QuizId = Questions[CurrentQuestionIndex - 1].QuizId;
            }
            if (CurrentQuestionIndex <= 1)
                return currentQuestion;
            if (CurrentQuestionIndex > Questions.Count)
            {
                Questions.Add(currentQuestion);
                CurrentQuestionIndex -= 1;
                return Questions[CurrentQuestionIndex - 1];
            }
            Questions[CurrentQuestionIndex - 1] = currentQuestion;
            CurrentQuestionIndex -= 1;
            return Questions[CurrentQuestionIndex - 1];
        }

        public static Question GetFirstQuestion()
        {
            if (Questions.Count == 0)
            {
                return new Question(text: "", answer1: "", answer2: "", answer3: null, answer4: null, correctAnswer: 0, time: 0, quizId: -1);
            }
            else
            {
                return Questions[0];
            }
        }

        public static Question GetNextQuestion(Question currentQuestion)
        {

            if (CurrentQuestionIndex >= Questions.Count)
            {
                Questions.Add(currentQuestion);
                CurrentQuestionIndex += 1;
                return new Question(text: "", answer1: "", answer2: "", answer3: null, answer4: null, correctAnswer: 0, time: 0, quizId: -1);
            }
            else
            {
                if (Questions.Count > 0 && CurrentQuestionIndex >= 0 && CurrentQuestionIndex <= Questions.Count)
                {
                    currentQuestion.IDQuestion = Questions[CurrentQuestionIndex - 1].IDQuestion;
                    currentQuestion.QuizId = Questions[CurrentQuestionIndex - 1].QuizId;
                }
                Questions[CurrentQuestionIndex - 1] = currentQuestion;
                CurrentQuestionIndex += 1;
                return Questions[CurrentQuestionIndex - 1];
            }

        }

        public static void UpdateLastQuestion(Question question)
        {
            if (CurrentQuestionIndex > Questions.Count)
            {
                Questions.Add(question);
                CurrentQuestionIndex += 1;
            }
            else
            {
                if (Questions.Count > 0 && CurrentQuestionIndex >= 0 && CurrentQuestionIndex <= Questions.Count)
                {
                    question.IDQuestion = Questions[CurrentQuestionIndex - 1].IDQuestion;
                    question.QuizId = Questions[CurrentQuestionIndex - 1].QuizId;
                }
                Questions[CurrentQuestionIndex - 1] = question;
                CurrentQuestionIndex += 1;
            }
        }

        public static void Save()
        {
            if (Quiz == null)
            {
                Quiz = new Quiz(title: QuizTitle, creatorid: Repository.creator.IDCreator ?? 0);
                Quiz = Repository.SaveQuiz(Quiz);
                foreach (var question in Questions)
                {
                    question.QuizId = Quiz.IDQuiz ?? 0;
                    Repository.SaveQuestion(question);
                }
            }
            else
            {
                Quiz.Title = QuizTitle;
                Repository.UpdateQuiz(Quiz);
                foreach (var question in Questions)
                {
                    int rows = Repository.UpdateQuestion(question);
                    if (rows == 0)
                    {
                        question.QuizId = Quiz.IDQuiz ?? 0;
                        Repository.SaveQuestion(question);
                    }

                }
            }

            ResetMachine();
        }

    }
}