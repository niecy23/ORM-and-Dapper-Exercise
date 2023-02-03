using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Building the Configuration with the database.
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connString = config.GetConnectionString("DefaultConnection");

            //Connecting to the IDbConnection Interface.
            IDbConnection conn = new MySqlConnection(connString);

            //Calling the GetAllDepartments and InsertDepartment Methods
            //then printing to the console using user input.
            var repoDept = new DapperDepartmentRepository(conn);

            var departments = repoDept.GetAllDepartments();

            Console.WriteLine("Here is a list of all of the Departments."); 
            foreach (var dept in departments)
            { 
                Console.WriteLine($"{dept.DepartmentID} {dept.Name}"); 
            }

            Console.WriteLine("Add/Type a new Department Name.");
            var newDept = Console.ReadLine();

            repoDept.InsertDepartment(newDept);
            departments = repoDept.GetAllDepartments();

            Console.WriteLine("Here is the list of Departments again with your new Department added.");

            foreach (var dept in departments)
            {
                Console.WriteLine($"{dept.DepartmentID} {dept.Name}");
            }   

            Thread.Sleep(1000);

            //Calling the GetAllProducts and CreateProduct Methods
            //then printing to the console using user input.
            var repoProd = new DapperProductRepository(conn);

            var products = repoProd.GetAllProducts();

            Console.WriteLine("Here is a list of all of the Products with their Name, Price, and CategoryID.");
            foreach (var prod in products)
            {
                Console.WriteLine($" {prod.Name}, ${prod.Price}, & {prod.CategoryID}");
            }

            Console.WriteLine("Let's add a product. What is the product's name?");
            var newProductName = Console.ReadLine();

            bool actualDouble;
            double newProductPrice;

            do
            {
                Console.WriteLine("What is the price of the product?");
                actualDouble = double.TryParse(Console.ReadLine(), out newProductPrice);
                if (!actualDouble)
                {
                    Console.WriteLine("That is not a number. Please try again using numbers in this format: 00.00");
                }

             } while (!actualDouble);

             bool actualInt;
             int newProductCID;

            do
            {
                Console.WriteLine("What is the Category ID of the product (1-10)?");
                actualInt = int.TryParse(Console.ReadLine(), out newProductCID);
                if (!actualInt)
                {
                    Console.WriteLine("That is not a number. Please try again using just numbers.");
                }

            } while (!actualInt);

            repoProd.CreateProduct(newProductName, newProductPrice, newProductCID);
            products = repoProd.GetAllProducts();

            Console.WriteLine("Here is the list of products again with your new product added."); 
            foreach (var prod in products)
            {
                Console.WriteLine($" {prod.Name}, ${prod.Price}, & {prod.CategoryID}");
            }

            Console.WriteLine("Let's update a product.");

            bool actualProdID;
            int prodID;

            do
            {
                Console.WriteLine("What is the ProductID that you want to update?");
                actualProdID = int.TryParse(Console.ReadLine(), out prodID);
                if (!actualProdID)
                {
                    Console.WriteLine("That is not a number. Please try again using just numbers.");
                }

            } while (!actualProdID);

            Console.WriteLine("What is the new product name?");
            var newName = Console.ReadLine();

            repoProd.UpdateProduct(prodID, newName);

            Console.WriteLine($"{prodID}, {newName}"); 

            Console.WriteLine("Thank you for your time! Have a great day!"); 

        }
    }
}
