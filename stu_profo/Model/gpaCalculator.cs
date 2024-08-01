using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stu_profo.Model
{

    class gpaCalculator
    {
        public static  double ConvertGradeToPoint(string grade)
        {
            switch (grade.ToUpper())
            {
                case "A+": return 4.0;
                case "A": return 4.0;
                case "A-": return 3.7;
                case "B+": return 3.3;
                case "B": return 3.0;
                case "B-": return 2.7;
                case "C+": return 2.3;
                case "C": return 2.0;
                case "C-": return 1.7;
                case "D+": return 1.3;
                case "D": return 1.0;
                case "F": return 0.0;
                default: return 0.0; // Unknown grade
            }
        }

        public static double  CalculateGPA(List<dataModel> dataSet)
        {
            double totalGradePoints = 0;
            double totalCredits = 0;

            foreach (dataModel data in dataSet)
            {
                double gradePoint = ConvertGradeToPoint(data.FinalGrade);
                double credits = double.TryParse(data.Points, out double result) ? result : 0;
                totalGradePoints += gradePoint * credits;
                totalCredits += credits;
            }

            double gpa = totalCredits > 0 ? totalGradePoints / totalCredits : 0;
            return Math.Round(gpa, 2);
        }
    }
}
