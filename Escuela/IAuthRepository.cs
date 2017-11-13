using Escuela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Escuela
{
    public interface IAuthRepository
    {
        // operations table students
        bool InsertStudent(Student student);
        bool UpdateStudent(Student originalStudent, Student updatedStudent);
        bool DeleteStudent(int id);
        Student GetStudent(int id);

        Course GetCourse(int courseId);

        Enrollment GetEnrollmentByStudentAndCourse(int studentId, int courseId);
        bool disableEnrollment(int enrollmentId);

        bool SaveAll();

        IQueryable<Student> GetAllStudents();
        IQueryable<Course> GetAllCourses();
        int EnrollStudentInCourse(int studentId, int courseId, Enrollment enrollment);
    }
}