using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Escuela.Models
{
    public class TeacherModel
    {
        public int TeacherID { get; set; }
        public string TeacherName { get; set; }

        public static List<TeacherModel> CreateTeachers()
        {
            List<TeacherModel> TeacherList = new List<TeacherModel>
            {
                new TeacherModel {TeacherID = 10248, TeacherName = "Juan Esteban" },
                new TeacherModel {TeacherID = 10249, TeacherName = "Bryan Bedoya" },
                new TeacherModel {TeacherID = 10250, TeacherName = "Stevens Sandoval" },
                new TeacherModel {TeacherID = 10251, TeacherName = "Andrea Zapata" },
                new TeacherModel {TeacherID = 10252, TeacherName = "Rami Malek" }
            };

            return TeacherList;
        }
    }
}