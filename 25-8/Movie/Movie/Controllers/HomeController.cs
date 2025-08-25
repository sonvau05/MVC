using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;
using System.Collections.Generic;

namespace MovieApp.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Movie> Movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "Inception", Description = "A thief who steals secrets...", Price = 9.99m, Director = "Christopher Nolan", TrailerUrl = "https://youtube.com/inception", WatchUrl = "https://netflix.com/inception", ImageUrl = "https://via.placeholder.com/200x300?text=Inception" },
            new Movie { Id = 2, Title = "The Matrix", Description = "A hacker discovers reality...", Price = 8.99m, Director = "Wachowski Sisters", TrailerUrl = "https://youtube.com/matrix", WatchUrl = "https://netflix.com/matrix", ImageUrl = "https://via.placeholder.com/200x300?text=Matrix" },
            new Movie { Id = 3, Title = "Interstellar", Description = "Explorers travel through a wormhole...", Price = 10.99m, Director = "Christopher Nolan", TrailerUrl = "https://youtube.com/interstellar", WatchUrl = "https://netflix.com/interstellar", ImageUrl = "https://via.placeholder.com/200x300?text=Interstellar" },
            new Movie { Id = 4, Title = "The Dark Knight", Description = "Batman faces the Joker...", Price = 9.49m, Director = "Christopher Nolan", TrailerUrl = "https://youtube.com/darkknight", WatchUrl = "https://netflix.com/darkknight", ImageUrl = "https://via.placeholder.com/200x300?text=DarkKnight" },
            new Movie { Id = 5, Title = "Pulp Fiction", Description = "Intertwined criminal stories...", Price = 7.99m, Director = "Quentin Tarantino", TrailerUrl = "https://youtube.com/pulpfiction", WatchUrl = "https://netflix.com/pulpfiction", ImageUrl = "https://via.placeholder.com/200x300?text=PulpFiction" }
        };

        public IActionResult Index()
        {
            return View(Movies);
        }

        public IActionResult Details(int id)
        {
            var movie = Movies.Find(m => m.Id == id);
            if (movie == null) return NotFound();
            return View(movie);
        }
    }
}