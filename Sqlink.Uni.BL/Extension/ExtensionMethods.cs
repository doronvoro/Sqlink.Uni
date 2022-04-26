using System;
using System.Collections.Generic;
using System.Linq;

namespace Sqlink.Uni.BL
{
    public static class ExtensionMethods
    {

        // public string TocourceTime(EnrollmentOperetion enrollmentOperetion)
        //{
        //    @($"{@course.StartTime.DayOfWeek} {@course.StartTime.ToString("HH:mm")} - {@course.EndTime.ToString("HH:mm")}" )

        //}

        public static string CourseRangeTime(this Course  course)
        {
            var time = $"{@course.StartTime.DayOfWeek} {@course.StartTime:HH:mm} - {@course.EndTime:HH:mm}";
            return time;
        }

    public static bool IsValidEnrollmentOperetion(this EnrollmentOperetion enrollmentOperetion, EnrollmentState currentEnrollmentState)
        {
            var opertions = new Dictionary<EnrollmentOperetion, Func<bool>> {
              
                { EnrollmentOperetion.AddCourse , () => !new []{ EnrollmentState.Payed , EnrollmentState.Cancelled }.Contains(currentEnrollmentState) },
                { EnrollmentOperetion.ClearAllCourses , () => !new []{ EnrollmentState.Payed , EnrollmentState.Cancelled }.Contains(currentEnrollmentState) },
                { EnrollmentOperetion.Complete , () => currentEnrollmentState ==  EnrollmentState.InProgress },
                { EnrollmentOperetion.Pay , () => currentEnrollmentState ==  EnrollmentState.Completed },
                { EnrollmentOperetion.Cancel ,  () => new []{ EnrollmentState.InProgress , EnrollmentState.Completed }.Contains(currentEnrollmentState)},
            };


            var isValid = opertions.Keys.Contains(enrollmentOperetion) &&  opertions[enrollmentOperetion]();

            return isValid;
        }


        public static bool IsValidEenrollmentState(this EnrollmentState currentEnrollmentState, EnrollmentState newEnrollmentState)
        {
            var states = new Dictionary<EnrollmentState, EnrollmentState[]>
            {
                {
                    EnrollmentState.InProgress,
                    new[]
                    {
                        EnrollmentState.Completed,
                        EnrollmentState.Cancelled,
                    }
                },
                {
                    EnrollmentState.Completed,
                    new[]
                    {
                        EnrollmentState.InProgress,
                        EnrollmentState.Cancelled,
                        EnrollmentState.Payed,
                    }
                }
            };

            var isValid = states.Keys.Contains(currentEnrollmentState) &&
                           states[currentEnrollmentState].Any(a => a == newEnrollmentState);

            return isValid;
        }

    }


}
