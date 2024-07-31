using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stu_profo.Model
{
    class dataModel
    {
        [JsonProperty("#")]
        public string Id { get; set; }

        public string Subject { get; set; }

        public string Special { get; set; }

        [JsonProperty("Exam Date")]
        public string ExamDate { get; set; }

        [JsonProperty("Course Work")]
        public string CourseWork { get; set; }

        public string Exam { get; set; }

        [JsonProperty("Final Grade")]
        public string FinalGrade { get; set; }

        public string Points { get; set; }

    }
}



//"#": "1",
//    "Subject": "DSE23.1F/CO/Introduction to Computer Science",
//    "Special": "",
//    "Exam Date": "2023-05-02",
//    "Course Work": "A",
//    "Exam": "A",
//    "Final Grade": "A",
//    "Points": "4"
