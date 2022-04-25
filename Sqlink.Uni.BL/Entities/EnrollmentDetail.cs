using System.Collections.Generic;

namespace Sqlink.Uni.BL
{
    public class EnrollmentDetail : BaseEntity
    {
        public Enrollment Enrollment { get; set; }
        public Student Student { get; set; }
        public  Course Course { get; set; }
    }


}
