using Escuela.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Escuela.Entities;

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

        public Enrollment GetEnrollmentByStudentAndCourse(int studentId, int courseId)
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

        public int EnrollStudentInCourse(int studentId, int courseId, Enrollment enrollment)
        {
            try
            {
                if (_ctx.Enrollments.Any(e => e.Course.Id == courseId && e.Student.Id == studentId))
                {
                    return 2;
                }

                _ctx.Database.ExecuteSqlCommand("INSERT INTO Enrollments VALUES (@p0, @p1, @p2)",
                    enrollment.Description, courseId.ToString(), studentId.ToString());

                return 1;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbex)
            {
                foreach (var eve in dbex.EntityValidationErrors)
                {
                    string line = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);

                    foreach (var ve in eve.ValidationErrors)
                    {
                        line = string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);

                    }
                }
                return 0;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}