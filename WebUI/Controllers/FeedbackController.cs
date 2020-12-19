using System.Web.Mvc;

using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Repository;
using Omu.ProDinner.WebUI.Utils;
using Omu.ProDinner.WebUI.ViewModels.Input;
using Omu.ValueInjecter;

namespace Omu.ProDinner.WebUI.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IRepo<Feedback> repo;

        public FeedbackController(IRepo<Feedback> repo)
        {
            this.repo = repo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return PartialView(new FeedbackInput());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FeedbackInput input)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(input);
            }

            input.Comments = HtmlUtil.SanitizeHtml(input.Comments);
            
            var feedback = new Feedback { Comments = input.Comments };
            feedback = repo.Insert(feedback);
            repo.Save();

            Session["lastFeedback"] = feedback.Id;
            return Json(new { });
        }

        public ActionResult Edit(int id)
        {
            return PartialView("Create", Mapper.Map<Feedback, FeedbackInput>(repo.Get(id)));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FeedbackInput input)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Create", input);
            }

            var feedback = repo.Get(input.Id.Value);
            feedback.Comments = HtmlUtil.SanitizeHtml(input.Comments);
            repo.Save();

            return Json(new { });
        }
    }
}
