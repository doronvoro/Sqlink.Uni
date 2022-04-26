using System.Collections.Generic;

namespace Sqlink.Uni.BL
{
    public interface IUniRepository
    {
        public IEnumerable<Course> GetEenrollmentCourses(bool b = false);
        public Enrollment GetCurrentEenrollment();
        Enrollment GetCreareEenrollment();
        EnrollmentDetail AddCourseToEenrollment(int courseId);
        void ClearCoursesFromEenrollment();
        void CancelEenrollment();
        void CompletedEenrollment();
        void PayEenrollment();
    }
}