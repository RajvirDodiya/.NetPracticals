using System.Collections.Generic;
using System.Web.Mvc;
using StudentFeedback.Models;

namespace StudentFeedback.Controllers
{
    public class FeedbackController : Controller
    {
        private static List<Feedback> feedbackList = new List<Feedback>
        {
            new Feedback
            {
                EnrollmentNumber = "92400120478",
                StudentName = "Rajvirsinh Dodiya",
                SubjectName = ".NET Technologies",
                Rating = 5,
                Comments = "Practical exercises were clear and informative."
            }
        };

        // GET: Feedback
        public ActionResult Index()
        {
            return View();
        }

        // GET: Feedback/Create
        public ActionResult Create()
        {
            return View(new Feedback());
        }

        // POST: Feedback/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedbackList.Add(feedback);
                return RedirectToAction("List");
            }

            return View(feedback);
        }

        // GET: Feedback/List
        public ActionResult List()
        {
            return View(feedbackList);
        }
    }
}