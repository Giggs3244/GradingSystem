using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Escuela.Entities
{
    public class Course
    {
        public Course()
        {
            Enrollments = new List<Enrollment>();
            CourseTutor = new Tutor();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Double Duration { get; set; }
        public string Description { get; set; }

        public Tutor CourseTutor { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}