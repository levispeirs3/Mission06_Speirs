using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission6Assignment.Models;
using System.Diagnostics;

namespace Mission6Assignment.Controllers
{
    public class HomeController : Controller
    {
        private MovieContext _context;

        public HomeController(MovieContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetToKnowJoel()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            ViewBag.Categories = _context.Categories.ToList();

            return View("AddMovie", new Movie());
        }

        [HttpPost]
        public IActionResult AddMovie(Movie response)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(response);
                _context.SaveChanges();

                return View("Confirmation", response);
            }
            else
            {
                ViewBag.Categories = _context.Categories.ToList();

                return View(response);
            }
        }
        public IActionResult Confirmation(Movie movie)
        {
            return View(movie);
        }

        public IActionResult MovieList()
        {
            var movies = _context.Movies
                                 .Include(m => m.Category)
                                 .ToList();

            return View(movies);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movieToEdit = _context.Movies
                .Single(m => m.MovieId == id);

            ViewBag.Categories = _context.Categories.ToList();

            return View("AddMovie", movieToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Movie response)
        {
            _context.Movies.Update(response);
            _context.SaveChanges();

            return RedirectToAction("MovieList");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var movieToDelete = _context.Movies
                .Single(m => m.MovieId == id);

            return View(movieToDelete);
        }

        [HttpPost]
        public IActionResult Delete(Movie response)
        {
            _context.Movies.Remove(response);
            _context.SaveChanges();

            return RedirectToAction("MovieList");
        }
    }
}
