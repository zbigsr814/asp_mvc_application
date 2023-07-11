using Microsoft.AspNetCore.Mvc;
using mvc_app_2.Models;

namespace mvc_app_2.Controllers
{
    public class HelloWorldController : Controller
    {
        private static List<DogViewModel> dogs = new List<DogViewModel>();
        public IActionResult Index()
        {
            return View(dogs);
        }
        
        public IActionResult Create() 
        {
            DogViewModel dogVm = new DogViewModel();
            return View(dogVm);
        }

        public IActionResult CreateDog(DogViewModel dogView)
        {
            dogs.Add(dogView);
            return RedirectToAction(nameof(Index));
            // return View("Index");
        }

        public string Hello()
        {   // komentarz
            return "Whos there";
        }
    }
}
