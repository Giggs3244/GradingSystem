using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Escuela.Entities
{
    public class Enrollment
    {
        public Enrollment()
        {
            Student = new Student();
            Course = new Course();
        }
        public int Id { get; set; }
        public String Description { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}