using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Sqlink.Uni.BL;
using System.Collections.Generic;

namespace Sqlink.Uni.Web.Pages
{
    //[IgnoreAntiforgeryToken(Order = 1001)]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUniRepository _uniRepository;
        
        public Enrollment Enrollment => _uniRepository.GetCurrentEenrollment();

        //public IEnumerable<Course> AllCourses => _uniRepository.GetEenrollmentCourses(false);

        public IEnumerable<Course> Courses => _uniRepository.GetEenrollmentCourses(false);

        public IEnumerable<Course> EnrollmentCourses => _uniRepository.GetEenrollmentCourses(true);


        public IndexModel(ILogger<IndexModel> logger, IUniRepository uniRepository)
        {
            _logger = logger;
            _uniRepository = uniRepository;
        }

        public void OnGet()
        {
        }


        public void OnPost()
        {
        }

 

        public RedirectToPageResult OnPostRegistration()
        {
            _uniRepository.GetCreareEenrollment();
             return RedirectToPage("Index");
        }
        public RedirectToPageResult OnPostClearAllCourses()
        {
            _uniRepository.ClearCoursesFromEenrollment();
            return RedirectToPage("Index");
        }

        public RedirectToPageResult OnPostCancelEenrollment()
        {
            _uniRepository.CancelEenrollment();
            return RedirectToPage("Index");
        }

        public RedirectToPageResult OnPostCompletedEenrollment()
        {
            _uniRepository.CompletedEenrollment();
            return RedirectToPage("Index");
        }

        public RedirectToPageResult OnPostPayEenrollment()
        {
            _uniRepository.PayEenrollment(); // .ClearAllCourses(
            return RedirectToPage("Index");
        }

        public RedirectToPageResult OnPostAddCourseToEenrollment(int id)
        {
            _uniRepository.AddCourseToEenrollment(id);
            return RedirectToPage("Index");
        }
    }
}
