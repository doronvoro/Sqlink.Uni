﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Sqlink.Uni.BL;
using System.Collections.Generic;
using System.Linq;

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
        //IsValidEnrollmentOperetion
        //Model.IsValidEnrollmentOperetion(EnrollmentOperetion.AddCourse) ? null : "disabled"  

        public string GetOperetionDisabled(EnrollmentOperetion enrollmentOperetion)
        {
            var enabled = false;
            if (Enrollment == null && enrollmentOperetion == EnrollmentOperetion.CreateRegistration)
            {
                enabled = true;
            }
            else if (Enrollment == null && enrollmentOperetion != EnrollmentOperetion.CreateRegistration)
            {
                enabled = false;
            }
            else if(!EnrollmentCourses.Any() &&  new[] { EnrollmentOperetion .ClearAllCourses, 
                                                         EnrollmentOperetion.Complete
                                                       }.Contains(enrollmentOperetion))
            {
                enabled = false;
            }
            else
            {
                enabled = enrollmentOperetion.IsValidEnrollmentOperetion(Enrollment.State);
            }

            return enabled ? null : "disabled";
        }


        //public bool IsValidEnrollmentOperetion(EnrollmentOperetion enrollmentOperetion )
        //{
        //    if (Enrollment == null && enrollmentOperetion == EnrollmentOperetion.CreateRegistration )
        //    {
        //        return true;
        //    }
        //    var isValid = enrollmentOperetion.IsValidEnrollmentOperetion(Enrollment.State);

        //    return isValid;
        //}

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
