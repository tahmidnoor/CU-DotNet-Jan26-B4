using System.Collections;

namespace HashTable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hashtable employeeTable = new Hashtable();
            employeeTable.Add(101, "Alice");
            employeeTable.Add(102, "Bob");
            employeeTable.Add(103, "Charlie");
            employeeTable.Add(104, "Diana");

            if (!employeeTable.ContainsKey(105))
            {
                employeeTable.Add(105, "Edward");
            }
            else
            {
                Console.WriteLine("Id already exist");
            }
            string empName = (string)employeeTable[102];
            Console.WriteLine(empName);

            foreach (DictionaryEntry entry in employeeTable)
            {
                Console.WriteLine($"ID: {entry.Key}, Name: {entry.Value}");
            }
            employeeTable.Remove(103);
            Console.WriteLine(employeeTable.Count);

        }
    }
}
