namespace StudentsAndCourses
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class StudentsAndCourses
    {
        public static void Main(string[] args)
        {
            var studentsInCourses = new SortedDictionary<string, SortedSet<Person>>();
            using (var reader = new StreamReader("../../students.txt"))
            {
                string input = reader.ReadLine();
                while (input != null)
                {
                    var inputParams = input.Split('|').Select(w => w.Trim()).ToArray();
                    string firstName = inputParams[0];
                    string lastName = inputParams[1];
                    string course = inputParams[2];
                    if (!studentsInCourses.ContainsKey(course))
                    {
                        studentsInCourses[course] = new SortedSet<Person>();
                    }

                    studentsInCourses[course].Add(new Person(firstName, lastName));
                    input = reader.ReadLine();
                }
            }

            foreach (var course in studentsInCourses)
            {
                Console.WriteLine("{0}: {1}", course.Key, string.Join(", ", course.Value));
            }
        }
    }
}
