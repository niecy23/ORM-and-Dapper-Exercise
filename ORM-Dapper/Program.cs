using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);

            var repo = new DapperDepartmentRepository(conn);

            var departments = repo.GetAllDepartments();

            Console.WriteLine("Here is a list of all of the Departments."); 
            foreach (var dept in departments)
            { 
                Console.WriteLine($"{dept.DepartmentID} {dept.Name}"); 
            }

            Console.WriteLine("Add/Type a new Department Name.");
            var newDept = Console.ReadLine();
            repo.InsertDepartment(newDept);
            departments = repo.GetAllDepartments();

            foreach (var dept in departments)
            {
                Console.WriteLine($"{dept.DepartmentID} {dept.Name}");
            }

        }
    }
}
