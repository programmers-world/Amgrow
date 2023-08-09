using aryamoneygrow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stockmantras.Models;
using System.Linq;

namespace aryamoneygrow
{
    public class NewsController : Controller
    {
        private readonly AppDBContext _ctx;

        public NewsController(AppDBContext ctx)
        {
            _ctx = ctx;
        }

        // GET: NewsController
        public ActionResult Index()
        {
            //News newList = new News();
            //var newList = _ctx.tblNews.ToList<News>().OrderByDescending(p=>p.N_ID).ToList();
            return View();
        }
        [HttpGet]
        public ActionResult DetailedNews(int id)
        {
            // Fetch the single news item based on the provided ID.
            var newsItem = _ctx.tblNews.FirstOrDefault(n => n.N_ID == id);

            // If the news item doesn't exist, handle it gracefully (e.g., redirect to the index page).
            if (newsItem == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.AllNews = newsItem;
            // Pass the single news item to the view.
            return View();
        }

        // GET: NewsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NewsController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: NewsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(News collection)
        {
            try
            {
                _ctx.tblNews.Add(collection);
                _ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: NewsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NewsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NewsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
