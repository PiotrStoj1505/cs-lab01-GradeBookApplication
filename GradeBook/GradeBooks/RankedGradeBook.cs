using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeBook.Enums;
using GradeBook.GradeBooks;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
        {
            Type = GradeBookType.Ranked;
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.");
                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.");
                return;
            }

            base.CalculateStudentStatistics(name);
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked grading requires at least 5 students.");
            }

            int threshold = (int)Math.Ceiling(Students.Count * 0.2);

            var sortedGrades = Students
                .OrderByDescending(s => s.AverageGrade)
                .Select(s => s.AverageGrade)
                .ToList();

            int betterStudents = sortedGrades.Count(g => g > averageGrade);

            if (betterStudents < threshold)
                return 'A';
            else if (betterStudents < threshold * 2)
                return 'B';
            else if (betterStudents < threshold * 3)
                return 'C';
            else if (betterStudents < threshold * 4)
                return 'D';
            else
                return 'F';
        }
    }
}