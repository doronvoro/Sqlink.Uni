using System;
using System.Collections.Generic;
using System.Linq;

namespace Sqlink.Uni.BL
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public string Lecturer { get; set; }
        public CourseYear Year { get; set; }
        public SemesterName Semester { get; set; }
        public bool IsMandatory { get; set; }

    }

}
