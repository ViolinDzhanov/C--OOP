using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Repositories.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        SubjectRepository subjects;
        StudentRepository students;
        UniversityRepository universities;

        public Controller()
        {
            subjects = new SubjectRepository();
            students = new StudentRepository();
            universities = new UniversityRepository();
        }
        public string AddStudent(string firstName, string lastName)
        {
            if (students.FindByName($"{firstName} {lastName}") != null)
            {
                return $"{firstName} {lastName} is already added in the repository.";
            }
              
            students.AddModel(new Student(students.Models.Count + 1, firstName, lastName));

            return $"Student {firstName} {lastName} is added to the {students.GetType().Name}!";
        }

        public string AddSubject(string subjectName, string subjectType)
        {
            if (subjectType != nameof(EconomicalSubject)
                && subjectType != nameof(HumanitySubject) 
                && subjectType != nameof(TechnicalSubject))
            {
                return $"Subject type {subjectType} is not available in the application!";
            }

            if (subjects.FindByName(subjectName) != null)
            {
                return $"{subjectName} is already added in the repository.";
            }

            ISubject subject;

            if (subjectType == nameof(EconomicalSubject))
            {
                subject = new EconomicalSubject(subjects.Models.Count +1, subjectName);
            }
            else if (subjectType == nameof(HumanitySubject))
            {
                subject = new HumanitySubject(subjects.Models.Count +1, subjectName);
            }
            else
            {
                subject = new TechnicalSubject(subjects.Models.Count +1, subjectName);
            }

            subjects.AddModel(subject);

            return $"{subjectType} {subjectName} is created and added to the {subjects.GetType().Name}!";
        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            if (universities.FindByName(universityName) != null)
            {
                return $"{universityName} is already added in the repository.";
            }

            ICollection<int> subjectsIds = new List<int>();

            foreach (var subject in requiredSubjects)
            {
                subjectsIds.Add(subjects.FindByName(subject).Id);
            }
            universities.AddModel(new University(universities.Models.Count + 1,
                universityName,
                category,
                capacity,
                subjectsIds));

            return $"{universityName} university is created and added to the {universities.GetType().Name}!";
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            string[] studentArg = studentName.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            IStudent student = students.FindByName(studentName);
            if (student == null)
            {
                return $"{studentArg[0]} {studentArg[1]} is not registered in the application!";
            }
            IUniversity university = universities.FindByName(universityName);
            if (university == null)
            {
                return $"{universityName} is not registered in the application!";
            }
            foreach (var currentSubject in university.RequiredSubjects)
            {
                if (!student.CoveredExams.Contains(currentSubject))
                {
                    return $"{studentName} has not covered all the required exams for {universityName} university!";
                }
            }
            if (student.University == university)
            {
                return $"{student.FirstName} {student.LastName} has already joined {university.Name}.";
            }

            student.JoinUniversity(university);
            return $"{student.FirstName} {student.LastName} joined {universityName} university!";
        }

        public string TakeExam(int studentId, int subjectId)
        {
            IStudent student = students.FindById(studentId);
            
            if (student == null)
            {
                return "Invalid student ID!";
            }

            ISubject subject = subjects.FindById(subjectId);
            if (subject == null)
            {
                return "Invalid subject ID!";
            }

            if(student.CoveredExams.Contains(subjectId))
            {
                return $"{student.FirstName} {student.LastName} has already covered exam of {subject.Name}.";
            }

            student.CoverExam(subject);

            return $"{student.FirstName} {student.LastName} covered {subject.Name} exam!";
        }

        public string UniversityReport(int universityId)
        {
            IUniversity university = universities.FindById(universityId);
            StringBuilder sb = new();

            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");
            int studentsCount = 0;

            foreach (var student in students.Models)
            {
                if (student.University == university)
                {
                    studentsCount++;
                }
            }
            sb.AppendLine($"Students admitted: {studentsCount}");
            sb.AppendLine($"University vacancy: {university.Capacity - studentsCount}");

            return sb.ToString().TrimEnd();
        }
    }
}
