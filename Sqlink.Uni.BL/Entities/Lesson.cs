using System;

namespace Sqlink.Uni.BL
{

    public class Lesson
    {
        public int CourseId { get; set; }
        public LessonType Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }


}
