using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Linq_Student
{
    class Program
    {
        static List<Student> students = new List<Student>
        {
            new Student {First = "Svetlana", Last = "Omelchenko", ID = 111, Scores = new List<int>{97, 92, 81,60 } },
            new Student {First = "Claire", Last = "O'Donnell", ID = 112, Scores = new List<int>{75, 84, 91, 39 } },
            new Student {First = "Sven", Last = "Montensen", ID = 113, Scores = new List<int>{88, 94, 65, 91 } },
            new Student {First = "Cesar", Last = "Garsia", ID = 114, Scores = new List<int>{97, 89, 85, 82 } },
            new Student {First = "Debra", Last = "Garsia", ID = 115, Scores = new List<int>{35, 72, 91, 70 } },
        };
        static void Main(string[] args)
        {
           int studentCount =(
                from student in students
                where student.Scores[0] > 90 && student.Scores[3] < 80
                select student).Count();

            int studentCountW = students.Where(st => st.Scores[0] > 90 && st.Scores[3] < 80).Count();

                Console.WriteLine("количество студентов : {0}, {1}", studentCount, studentCountW);

            var studentList = (
                from student in students
                where /*student.Scores[0] > 90 &&*/ student.Scores[3] < 80
                orderby student.Scores[0] descending
                select student).ToList();

            var studentQuery2 =
          from student in students
          group student by student.Last[0];

            var studentQuery3 =
                from student in students
                group student by student.Last[0];

            //foreach (var groupOfStudents in studentQuery3)
            //{
            //    Console.WriteLine(groupOfStudents.Key);
            //    foreach (Student student in groupOfStudents)
            //    {
            //        Console.WriteLine("{0}, {1}", student.Last, student.First);
            //    }
            //}

            var studentQuery4 =
                from student in students
                group student by student.Last[0] into studentGroup
                orderby studentGroup.Key
                select studentGroup;


            //foreach (var groupOfStudents in studentQuery4)
            //{
            //    Console.WriteLine(groupOfStudents.Key);
            //    foreach (Student student in groupOfStudents)
            //    {
            //        Console.WriteLine("{0}, {1}", student.Last, student.First);
            //    }
            //}

            var studentQuery5 =
             from student in students
             let totalScore = student.Scores[0] + student.Scores[1] + student.Scores[2] + student.Scores[3]
             where totalScore / 4 < student.Scores[3]
             select student.Last + " " + student.First;


            //foreach (string s in studentQuery5)
            //{
            //    Console.WriteLine(s);

            //}

            var studentQuery6 =
         from student in students
         let totalScore = student.Scores[0] + student.Scores[1] + student.Scores[2] + student.Scores[3]
         select totalScore;

            double averageScore = studentQuery6.Average();
            //Console.WriteLine("Class average score = {0}", averageScore);

            IEnumerable<string> studentQuery7 =
                 from student in students
                 where student.Last == "Garsia"
                 select student.First;

            //Console.WriteLine("The Garsias in the class are:");
            //foreach (string s in studentQuery7)
            //{
            //    Console.WriteLine(s);

            //}

            var studentQuery8 =
                 from student in students
                 let x = student.Scores[0] + student.Scores[1] + student.Scores[2] + student.Scores[3]
                 where x > averageScore
                 select new { id = student.ID, score = x};

            foreach (var item in studentQuery8)
            {
                Console.WriteLine("Student ID: {0}, Score: {1}", item.id, item.score);

            }

            Console.ReadKey();
            
        }
    }
}
