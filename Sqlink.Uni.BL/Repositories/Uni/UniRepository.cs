using System;
using System.Collections.Generic;
using System.Linq;

namespace Sqlink.Uni.BL
{ 
    public class UniRepository : IUniRepository
    {
        private IGenericRepository<EnrollmentDetail> _enrollmentDetailRepository;
        private IGenericRepository<Enrollment> _enrollmentRepository;
        private IGenericRepository<Student> _studentRepository;
        private IGenericRepository<Course> _courseRepository;

        public UniRepository(IGenericRepositoryFactory<EnrollmentDetail> enrollmentDetailRepositoryFactory,
                             IGenericRepositoryFactory<Enrollment> enrollmentRepositoryFactory,
                             IGenericRepositoryFactory<Student> studentRepositoryFactory,
                             IGenericRepositoryFactory<Course> courseRepositoryFactory,
                             IGenericRepositoryFactory<Enrollment> genericRepositoryFactory )
        {
            _enrollmentDetailRepository = enrollmentDetailRepositoryFactory.GetRepository();
            _enrollmentRepository = enrollmentRepositoryFactory.GetRepository();
            _studentRepository = studentRepositoryFactory.GetRepository();
            _courseRepository = courseRepositoryFactory.GetRepository();
        }

        public IEnumerable<Course> GetEenrollmentCourses(bool b = false)
        {
            var enrollment = GetCurrentEenrollment();

             

            var courseIds = _enrollmentDetailRepository.GetAll()
                                                        .Where(w => w.Enrollment.Id == enrollment?.Id)
                                                        .Select(s => s.Course.Id);


            var courses = _courseRepository.GetAll()
                                            .Where(w => {

                                                var r = b ? courseIds.Contains(w.Id) :
                                                     !courseIds.Contains(w.Id);
                                                return r;
 


                                            })
                                            .OrderBy(o => o.IsMandatory)
                                            .ThenBy(o => o.Year)//
                                            .ThenBy(o => o.Semester);//

            return courses;
        }


       

        public EnrollmentDetail AddCourseToEenrollment(int courseId)
        {
            var course = _courseRepository.GetById(courseId);
            if (course == null)
            {
                throw new Exception($"course not found. courseId= {courseId}");
            }

            var enrollment = GetCurrentEenrollment();
            if (enrollment == null)
            {
                throw new Exception($"enrollment not found"); //todo:create custom exception. print ids
            }

            var enrollmentDetail = new EnrollmentDetail
            {
                Course = course,
                Enrollment = enrollment,
                Student = GetStudentByCurrentUser()
            };

            //todo: check course time..
            var existsEnrollment = _enrollmentDetailRepository.GetAll()
                                       .Where(w => w.Course.Id == enrollmentDetail.Course.Id &&
                                                   w.Enrollment.Id == enrollmentDetail.Enrollment.Id &&
                                                   w.Student.Id == enrollmentDetail.Student.Id)
                                       .FirstOrDefault();

            if (existsEnrollment != null)
            {
                throw new Exception($"EnrollmentDetail already exists "); //todo: print ids
            }


            _enrollmentDetailRepository.Insert(enrollmentDetail);
            _enrollmentDetailRepository.Save();

            return enrollmentDetail;
        }

        public Enrollment GetCreareEenrollment()
        {
            var enrollment = GetCurrentEenrollment();

            if (enrollment != null)
            {
                return enrollment;
            }

            enrollment = new Enrollment
            {
                State = EnrollmentState.InProgress
            };

            _enrollmentRepository.Insert(enrollment);
            _enrollmentRepository.Save();

            return enrollment;
        }

        public void ClearCoursesFromEenrollment()
        {
            var enrollment = GetCurrentEenrollment();

            DeleteEenrollmentDetails(enrollment.Id);

            _enrollmentDetailRepository.Save();
        }

        public void CancelEenrollment()
        {
            var enrollment = GetCurrentEenrollment();

            UpdateEenrollmentState(enrollment, EnrollmentState.Cancelled);

            DeleteEenrollmentDetails(enrollment.Id);

            //todo: unit of work
            _enrollmentRepository.Save();
            _enrollmentDetailRepository.Save();
        }


        public void CompletedEenrollment()
        {
            var enrollment = GetCurrentEenrollment();

            UpdateEenrollmentState(enrollment, EnrollmentState.Completed);

            _enrollmentRepository.Save();
        }
        public void PayEenrollment()
        {
            var enrollment = GetCurrentEenrollment();

            UpdateEenrollmentState(enrollment, EnrollmentState.Payed);

            _enrollmentRepository.Save();
        }


        private Student GetStudentByCurrentUser()
        {
            var student = _studentRepository.GetAll().FirstOrDefault();
            return student;
        }

        public  Enrollment GetCurrentEenrollment()
        {

           // var closeEnrollment = new[] { EnrollmentState.Payed, EnrollmentState.Completed };

            //var enrollment = _enrollmentRepository.GetAll()
            //                                      .Where(w => !closeEnrollment.Contains(w.State))
            //                                      //todo: && w.UserId == ??
            //                                      .FirstOrDefault();

            var enrollment = _enrollmentRepository.GetAll()
                                                 .FirstOrDefault();



            return enrollment;
        }


        private void DeleteEenrollmentDetails(int enrollmentId)
        {

            var enrollmentDetails = _enrollmentDetailRepository.GetAll()
                                                           .Where(w => w.Enrollment.Id == enrollmentId);

            foreach (var enrollmentDetail in enrollmentDetails)
            {
                _enrollmentDetailRepository.Delete(enrollmentDetail.Id);
            }


        }

//InProgress → Completed.a
//InProgress → Cancelled.b
//Completed → InProgress.c
//Completed → Cancelled.d
//Completed → Payed
       

        private void UpdateEenrollmentState(Enrollment enrollment, EnrollmentState enrollmentState)
        {
            var isValid =enrollment.State.IsValidEenrollmentState( enrollmentState);
            if(!isValid)
            {
                throw new Exception($"cannot update enrollment state. currrent state = {enrollment.State} new enrollmentState ={enrollmentState}");
            }

            _enrollmentRepository.UpdateById(enrollment.Id, (e) => e.State = enrollmentState);
        }
      

    

    }


}
