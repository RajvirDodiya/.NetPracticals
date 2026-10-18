using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StudentFeedback.Models;

namespace StudentFeedback.Controllers
{
    public class FeedbackController : Controller
    {
        // In-memory collection storing student feedback entries for the session
        private static readonly List<Feedback> feedbackRepository = new List<Feedback>
        {
            new Feedback
            {
                EnrollmentNumber = "92200103045",
                StudentName = "Rajvir Sinh",
                SubjectName = ".NET Framework & C# Programming",
                Rating = 5,
                Comments = "Practical sessions provided great insight into ASP.NET MVC architecture, controllers, and view rendering."
            },
            new Feedback
            {
                EnrollmentNumber = "92200103018",
                StudentName = "Dev Patel",
                SubjectName = "Enterprise Web Development",
                Rating = 4,
                Comments = "Enjoyed the hands-on lab assignments on Model-View-Controller pattern and entity validation."
            }
        };

        // GET: Feedback / Index (Portal landing page)
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Feedback / Create (Form view to submit new feedback)
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Feedback());
        }

        // POST: Feedback / Create (Processes and validates feedback submission)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedbackRepository.Add(feedback);
                return RedirectToAction("List");
            }

            return View(feedback);
        }

        // GET: Feedback / List (Renders summary table of all submitted responses)
        [HttpGet]
        public ActionResult List()
        {
            return View(feedbackRepository.ToList());
        }
    }
}