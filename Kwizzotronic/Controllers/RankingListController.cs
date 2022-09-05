using Kwizzotronic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kwizzotronic.Controllers
{
    public class RankingListController : BaseController
    {
        // GET: RankingList
        public ActionResult Index()
        {
            return View(QuizStateMachine.QuizInstances);
        }


        public ActionResult GetRankingList(int? instanceId)
        {
            QuizInstance quizInstance = GetQuizInstance(instanceId ?? 0);
            if (quizInstance.IDQuizInstance != null)
            {
                var rankingList = Repository.GetRankListForQuiz((int)quizInstance.IDQuizInstance);
                if (rankingList != null)
                {
                    return View(rankingList.OrderBy(o => o.Position));
                }

            }


            return RedirectToAction("Index", "RankingList");
        }

        private QuizInstance GetQuizInstance(int id)
        {
            QuizInstance quizInstance = null;

            foreach (var item in QuizStateMachine.QuizInstances)
            {
                if (item.IDQuizInstance == id)
                {
                    quizInstance = item;
                }
            }
            return quizInstance;
        }

    }
}