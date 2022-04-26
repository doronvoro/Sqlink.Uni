using System.Collections.Generic;

namespace Sqlink.Uni.BL
{
    public interface IUniRepository
    {
        public IEnumerable<Course> GetCourses(bool onlyEenrollmentCourses = false);// todo: use type as a parmter or create 2 function
        public Enrollment GetCurrentEenrollment();
        Enrollment GetCreareEenrollment();
        public void AddCourseToEenrollment(int courseId, out string message); //todo: return result with message
        void ClearCoursesFromEenrollment();
        void CancelEenrollment();
        void CompletedEenrollment();
        void PayEenrollment();
    }
}