using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stu_profo.Model
{
    public class Course
    {
        //public string CourseName { get; set; }
        //public string CourseCode { get; set; }
        //public string CourseID { get; set; }
        //public double GPA { get; set; }  // Updated to a property


        public string Id { get; set; }

        public string Subject { get; set; }

        public string CourseWork { get; set; }

        public string Exam { get; set; }

        public string FinalGrade { get; set; }

        public string Points { get; set; }
    }

}
