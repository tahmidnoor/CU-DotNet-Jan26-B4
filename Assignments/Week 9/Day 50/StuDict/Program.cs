namespace StuDict
{
    class Student
    {
        public int SID { get; set; }
        public string SName { get; set; }

        public Student(int id,  string name)
        {
            SID = id;
            SName = name;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Student s)
            {
                return this.SID == s.SID && this.SName == s.SName;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SID, SName);
        }
    }

    internal class Program
    {
        static void AddStudent(Dictionary<Student, int> dict, Student s, int marks)
        {
            if (dict.ContainsKey(s))
            {
                if(marks > dict[s])
                {
                    dict[s] = marks;
                }
            }
            else
            {
                dict.Add(s, marks);
            }
        }

        static void Main(string[] args)
        {
            Dictionary<Student, int> students = new Dictionary<Student, int>();

            AddStudent(students, new Student(1, "Tahmid"), 70);
            AddStudent(students, new Student(2, "Shreya"), 80);
            AddStudent(students, new Student(1, "Tahmid"), 85);
            AddStudent(students, new Student(2, "Shreya"), 95);

            Console.WriteLine("Latest Student Data:");

            foreach (var item in students)
            {
                Console.WriteLine($"{item.Key.SName}\t: {item.Value}");
            }
        }
    }
}
