using Escuela.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Escuela.Entities;
using System.Data.SqlClient;

namespace Escuela
{
    public class AuthRepository : IDisposable, IAuthRepository
    {
        private AuthContext _ctx;

        private UserManager<IdentityUser> _userManager;

        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }

        public bool InsertStudent(Student student)
        {
            try
            {
                _ctx.Students.Add(student);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateStudent(Student originalStudent, Student updatedStudent)
        {
            _ctx.Entry(originalStudent).CurrentValues.SetValues(updatedStudent);
            return true;
        }

        public bool DeleteStudent(int id)
        {
            try
            {
                var entity = _ctx.Students.Find(id);
                if (entity != null)
                {
                    _ctx.Students.Remove(entity);
                    return true;
                }
            }
            catch
            {
                // TODO Logging
            }

            return false;
        }

        public Student GetStudent(int studentId)
        {
            return _ctx.Students.Find(studentId);
        }

        public Course GetCourse(int courseId)
        {
            throw new NotImplementedException();
        }

        public Enrollment GetEnrollmentByCourse(int courseId)
        {
            throw new NotImplementedException();
        }

        public bool disableEnrollment(int enrollmentId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Student> GetAllStudents()
        {
            return _ctx.Students.AsQueryable();
        }

        public IQueryable<Course> GetAllCourses()
        {
            return _ctx.Courses
                    .Include("CourseTutor")
                    .AsQueryable();
        }

        public int EnrollStudentInCourse(int studentIdParam, int courseIdParam, String descriptionParam)
        {
            try
            {
                if (_ctx.Enrollments.Any(e => e.Course.Id == courseIdParam && e.Student.Id == studentIdParam))
                {
                    return 2;
                }

                var studentId = new SqlParameter("@student_id", studentIdParam);
                var courseId = new SqlParameter("@course_id", courseIdParam);
                var description = new SqlParameter("@description", descriptionParam);

                _ctx.Database.ExecuteSqlCommand("EXEC sp_enrollment_insert @student_id , @course_id , @description", studentId, courseId, description);

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        public IQueryable<Student> GetEnrolledStudentsInCourse(int courseId)
        {
            return _ctx.Students
                    .Include("Enrollments")
                    .Where(c => c.Enrollments.Any(s => s.Course.Id == courseId))
                    .AsQueryable();
        }
    }
}