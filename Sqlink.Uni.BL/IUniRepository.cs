using System.Collections.Generic;

namespace Sqlink.Uni.BL
{
    public interface IUniRepository
    {
        IEnumerable<Course> GetDefaultSortedCourses();
    }
}