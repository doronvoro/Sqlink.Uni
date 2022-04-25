using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Sqlink.Uni.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sqlink.Uni.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUniRepository _uniRepository;

        public IndexModel(ILogger<IndexModel> logger, IUniRepository uniRepository)
        {
            _logger = logger;
            _uniRepository = uniRepository;
        }


        public IEnumerable<Course> Courses
        {
            get
            {
                return _uniRepository.GetDefaultSortedCourses();
            }
        }

        //public List<Person> People => Data;

        public RedirectToPageResult OnCourseAdd(int id)
        {
            // People.RemoveAll(p => p.Id == id);
            return RedirectToPage("Index");
        }

        public void OnGet()
        {

        }
    }
}
