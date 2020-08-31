using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using dashboard.DAL;
using dashboard.Models;
using PagedList;

namespace dashboard.Controllers
{
    public class SurveyController : Controller
    {
        private readonly SurveyContext db = new SurveyContext();
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var surveys = from s in db.Surveys
                          select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                // Status is een enum en heeft dus geen contains methode nog. Moet nog geimplementeerd worden als daarop 
                // gezocht moet kunnen worden. Kan er wel op sorten.
                surveys = surveys.Where(s => s.Name.Contains(searchString)
                                       || s.Owner.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    surveys = surveys.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    surveys = surveys.OrderBy(s => s.StartDate);
                    break;
                case "date_desc":
                    surveys = surveys.OrderByDescending(s => s.StartDate);
                    break;
                default:
                    surveys = surveys.OrderBy(s => s.Status);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(surveys.ToPagedList(pageNumber, pageSize));
        }
        // GET: Survey/Details/5
        public ActionResult Questions(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // GET: Survey/Create
        public ActionResult Create()
        {
            ViewBag.OwnerID = new SelectList(db.Owners, "ID", "Name");
            return View();
        }

        // POST: Survey/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Status,StartDate,EndDate,OwnerID")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Surveys.Add(survey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerID = new SelectList(db.Owners, "ID", "Name", survey.OwnerID);
            return View(survey);
        }

        // GET: Survey/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerID = new SelectList(db.Owners, "ID", "Name", survey.OwnerID);
            return View(survey);
        }

        // POST: Survey/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Status,StartDate,EndDate,OwnerID")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                /* poging om de oude status te vergelijken met de nieuwe. 
                 * 
                if (survey.Status.CompareTo(db.Surveys.Find(survey.ID).Status) < 0)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("Status: {0} een stap terug van de huidige status.", survey.Status)),
                        ReasonPhrase = "Deze statii zijn sequentieel en kunnen niet worden teruggedraaid"
                    };
                    throw new System.Web.Http.HttpResponseException(resp);
                }
                */

                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerID = new SelectList(db.Owners, "ID", "Name", survey.OwnerID);
            return View(survey);
        }


        // GET: Survey/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }
        // POST: Owner/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Survey survey = db.Surveys.Find(id);
                db.Surveys.Remove(survey);
                db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Index", new { id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
