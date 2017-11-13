using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Escuela.Entities
{
    public class Tutor
    {
        public Tutor()
        {
            Courses = new List<Course>();
        }
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Course> Courses;
    }
}