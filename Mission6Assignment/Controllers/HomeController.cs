using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission6Assignment.Models;

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
            return View("AddMovie");
        }

        [HttpPost]
        public IActionResult AddMovie(Movie response)
        {
            _context.Movies.Add(response);
            _context.SaveChanges();

            return View("Confirmation", response);
        }

        public IActionResult Confirmation(Movie movie)
        {
            return View(movie);
        }
    }
}