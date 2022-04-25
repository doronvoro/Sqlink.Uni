using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Sqlink.Uni.BL;
using Sqlink.Uni.Mvc.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sqlink.Uni.Mvc.Controllers
{



    public class IndexModel : PageModel
    {
        //private static int _personId = 0;
        //private static readonly List<Person> Data = new Faker<Person>()
        //    .RuleFor(m => m.Id, f => _personId++)
        //    .RuleFor(p => p.Name, f => f.Name.FullName())
        //    .RuleFor(m => m.Age, f => f.Random.Number(18, 65))
        //    .RuleFor(m => m.CompanyName, f => f.Company.CompanyName())
        //    .RuleFor(m => m.Country, f => f.Address.Country())
        //    .RuleFor(m => m.City, f => f.Address.City())
        //    .Generate(20);

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }


        public IEnumerable<Course> Courses { get; set; }

        //public List<Person> People => Data;

        public RedirectToPageResult OnPostRemove(int id)
        {
           // People.RemoveAll(p => p.Id == id);
            return RedirectToPage("Index");
        }
    }


    public class EnrollmentModel :  PageModel
    {
        public IEnumerable<Course> Courses { get; set; }

        public void Remove(int courseId)
        {
        }
    }

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var courses = Course.GetCourses();

            var model = new EnrollmentModel
            {
                Courses = courses
            };

            return View(model);
        }

        //public RedirectToPageResult OnPostRemove(int id)
        //{
        //    return default;
        //    //People.RemoveAll(p => p.Id == id);
        //    //return RedirectToPage("Index");
        //}

        //public void Remove(int courseId)
        //{
        //}
            
        //public void AddCourse(int courseId)
        //{
        //    //var courses = Course.GetCourses();

        //    //var model = new EnrollmentModel
        //    //{
        //    //    Courses = courses
        //    //};

        //    //return View(model);
        //}
        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
