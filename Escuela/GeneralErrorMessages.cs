using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Escuela
{
    public class GeneralErrorMessages
    {

        public GeneralErrorMessages()
        {
            GeneralErrorMessage = "An error occurred. Please try again later.";
            StudentsNotFound = "Students not found.";
            CoursesNotFound = "Courses not found.";
            StudentNotFound = "Student is not found.";
            StudentNotReadBody = "Could not read student from body";
            EnrollmentNotReadBody = "Could not read enrollment from body";
            StudentNotModified = "Student is not modified";
            StudentIsEnrollment = "Student is enrollment in this course";
        }

        public String GeneralErrorMessage { get; set; }
        public String StudentsNotFound { get; set; }
        public String StudentNotFound { get; set; }
        public String StudentNotReadBody { get; set; }
        public String StudentNotModified { get; set; }
        public String EnrollmentNotReadBody { get; set; }
        public String StudentIsEnrollment { get; set; }
        public String CoursesNotFound { get; set; }

    }
}