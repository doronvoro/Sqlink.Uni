﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Sqlink.Uni.BL
{
    public class InMemoryDb
    {

        public InMemoryDb()
        {
            // todo: use Lazy<T>
            Courses = new ConcurrentDictionary<int,Course>(GetCourses());
            Enrollments = new ConcurrentDictionary<int, Enrollment>();
            EnrollmentDetails = new ConcurrentDictionary<int, EnrollmentDetail>();
            Students = new ConcurrentDictionary<int, Student>();
        }


        public ConcurrentDictionary<int, T> GetConcurrentDictionary<T>() where T : BaseEntity
        {
            var type = typeof(T);

            if(type == typeof(Course))
            {
                return Courses as ConcurrentDictionary<int, T>;
            }

            if (type == typeof(Enrollment))
            {
                return Enrollments as ConcurrentDictionary<int, T>;
            }
            if (type == typeof(EnrollmentDetail))
            {
                return EnrollmentDetails as ConcurrentDictionary<int, T>;
            }
            if (type == typeof(Student))
            {
                return Students as ConcurrentDictionary<int, T>;
            }

            throw new Exception($"{type} not found");

        }

        public static IEnumerable<KeyValuePair<int, Course>> GetCourses()
        {
            var rnd = new Random();
            foreach (var item in Enumerable.Range(1, 8))
            {
                yield return new KeyValuePair<int, Course>(item, new Course
                {
                    Lecturer = $"Lecturer {item}",
                    Name = $"Name {item}",
                    Id = item,
                    Semester = (SemesterName)rnd.Next(1, 3),
                    Year = (CourseYear)rnd.Next(1, 3),
                    IsMandatory = rnd.Next(0,1) == 1  
                });
            }

        }

        public ConcurrentDictionary<int,Enrollment> Enrollments { get;private set; }

        public ConcurrentDictionary<int, EnrollmentDetail > EnrollmentDetails { get; private set; }

        public ConcurrentDictionary<int, Student> Students { get; private set; }

        public ConcurrentDictionary<int, Course> Courses { get; private set; }
    }


}
