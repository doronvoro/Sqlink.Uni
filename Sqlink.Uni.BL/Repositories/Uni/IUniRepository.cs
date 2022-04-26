using System.Collections.Generic;

namespace Sqlink.Uni.BL
{
    public interface IUniRepository
    {
        public IEnumerable<Course> GetEenrollmentCourses(bool b = false);
        public Enrollment GetCurrentEenrollment();
        Enrollment GetCreareEenrollment();
        public void AddCourseToEenrollment(int courseId, out string message); //todo: return result with message
        void ClearCoursesFromEenrollment();
        void CancelEenrollment();
        void CompletedEenrollment();
        void PayEenrollment();
    }
}