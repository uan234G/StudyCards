using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyCards.Models;

namespace StudyCards.Controllers {
    public class HomeController : Controller {
        private MyContext dbContext;
        public HomeController (MyContext context) {
            dbContext = context;
        }
        private int? UserSession {
            get { return HttpContext.Session.GetInt32 ("UserId"); }
            set { HttpContext.Session.SetInt32 ("UserId", (int) value); }
        }

        public IActionResult Index () {
            List<Book> AllBooks = dbContext.Books.ToList ();
            return View (AllBooks);
        }

        [HttpGet ("New/Book")]
        public IActionResult NewBook () {
            return View ();
        }

        [HttpPost ("New/Book")]
        public IActionResult NewBookSubmit (Book NewBook) {
            if (ModelState.IsValid) {
                dbContext.Books.Add (NewBook);
                dbContext.SaveChanges ();
                return RedirectToAction ("Book", new { Title = NewBook.Title });
            }
            return View ("NewBook");
        }

        [HttpGet ("Book/{Title}")]
        public IActionResult Book (string Title) {
            Book CurrentBook = dbContext.Books.Include (q => q.Chapters).ThenInclude (a => a.FlashCards).FirstOrDefault (q => q.Title == Title);
            return View (CurrentBook);
        }

        [HttpGet ("Chapter/{ChId}")]
        public IActionResult Chapter (int ChId) {
            NumList.Clear ();
            Chapter CurrentChapter = dbContext.Chapters.Include (a => a.Book).Include (a => a.FlashCards).FirstOrDefault (a => a.ChapterId == ChId);
            ViewBag.ChapterId = CurrentChapter.ChapterId;
            return View (CurrentChapter);
        }

        [HttpGet ("New/Chapter/{BookId}")]
        public IActionResult NewChapter (int BookId) {
            Book leCurrent = dbContext.Books.FirstOrDefault (a => a.BookId == BookId);
            ViewBag.BookId = leCurrent.BookId;
            return View ();
        }

        [HttpPost ("new/chapter/{BookId}")]
        public IActionResult ChapterSubmit (Chapter NewChapter, int BookId) {
            if (ModelState.IsValid) {
                NewChapter.BookId = BookId;
                dbContext.Add (NewChapter);
                dbContext.SaveChanges ();
                return RedirectToAction ("Chapter", new { ChId = NewChapter.ChapterId });
            }
            return View ("NewChapter");
        }

        [HttpGet ("New/Word/{ChID}")]
        public IActionResult NewWord (int ChId) {
            Chapter Current = dbContext.Chapters.FirstOrDefault (q => q.ChapterId == ChId);
            ViewBag.ChId = Current.ChapterId;
            return View ();
        }

        [HttpPost ("Add/FlashCard/{ChId}")]
        public IActionResult NewWordSubmit (FlashCard NewCard, int ChId) {
            if (ModelState.IsValid) {
                NewCard.ChapterId = ChId;
                dbContext.FlashCards.Add (NewCard);
                dbContext.SaveChanges ();
                return RedirectToAction ("NewWord", new { ChId = ChId });
            }
            return View ("NewWord");
        }

        [HttpGet ("Review/{ChId}")]
        public IActionResult ChapterReview (int ChId) {
            Chapter leCurrent = dbContext.Chapters.Include (a => a.FlashCards).FirstOrDefault (a => a.ChapterId == ChId);
            return View (leCurrent);
        }

        [HttpGet ("Complete/{fId}")]
        public IActionResult Complete (int fId) {
            FlashCard leCurrent = dbContext.FlashCards.FirstOrDefault (a => a.FlashCardId == fId);
            leCurrent.IsComplete = true;
            dbContext.SaveChanges ();
            return RedirectToAction ("Study", new { ChId = leCurrent.ChapterId });
        }
        static Random rnd = new Random ();
        static List<int> NumList = new List<int> ();
        static FlashCard fCard = new FlashCard ();
        [HttpGet ("Study/{ChId}")]
        public IActionResult Study (int ChId) {
            List<FlashCard> studycard = dbContext.FlashCards.Include (a => a.Chapter).Where (a => a.ChapterId == ChId).Where (z => z.IsComplete == false).ToList ();
            int r = rnd.Next (0, studycard.Count);
            if (NumList.Contains (r)) {
                r = rnd.Next (0, studycard.Count);
            }
            fCard = studycard[r];
            NumList.Add (r);
            ViewBag.ChapterId = ChId;
            ViewBag.Num = fCard.FlashCardId;
            return View (fCard);
        }

        public IActionResult Privacy () {
            return View ();
        }

        [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error () {
            return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}