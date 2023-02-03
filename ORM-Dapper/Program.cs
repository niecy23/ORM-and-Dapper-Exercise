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

            Console.WriteLine($"Would you like to add a new Department: Yes or No?");
            var addDept = Console.ReadLine();

            bool addingDept = true;

            bool no = true;

            while (addingDept)
            {
                if (addDept == null || addDept == "")
                {
                    Console.WriteLine("Please try again.");
                    Console.WriteLine("Would you like to add a new Department: Yes or No?");
                    addDept = Console.ReadLine();

                    if (addDept.ToLower() == "yes")
                    {
                        addingDept = false;
                    }
                    else if (addDept.ToLower() == "no")
                    {
                        no = false;
                        continue;
                    }
                    else
                    {
                        addingDept = true;
                    }

                }

                while (no)
                {
                    Console.WriteLine("Add/Type a new Department Name.");
                    var newDept = Console.ReadLine();

                    while (!addingDept)
                    {
                        if (newDept == null || newDept == "")
                        {
                            Console.WriteLine("Please try again.");
                            Console.WriteLine("Add/Type a new Department Name.");
                            newDept = Console.ReadLine();
                        }

                        repoDept.InsertDepartment(newDept);
                        departments = repoDept.GetAllDepartments();

                        Console.WriteLine("Here is the list of Departments again with your new Department added.");

                        foreach (var dept in departments)
                        {
                            Console.WriteLine($"{dept.DepartmentID} {dept.Name}");
                        }   

                        Console.WriteLine("Would you like to add another Department: Yes or No?");
                        addDept = Console.ReadLine();

                        if (addDept == null || addDept == "")
                        { 
                            Console.WriteLine("Please try again.");
                            Console.WriteLine("Would you like to add a another Department: Yes or No?");
                            addDept = Console.ReadLine();

                            if (addDept.ToLower() == "yes")
                            {
                                addingDept = true;
                            }
                            else if (addDept.ToLower() == "no")
                            {
                                no = false;
                                continue;
                            }
                            else
                            {
                                addingDept = true;
                            }
                        }
                    }
                       
                }
            }

            

            Thread.Sleep(500);

            //Calling the GetAllProducts and CreateProduct Methods
            //then printing to the console using user input.
            var repoProd = new DapperProductRepository(conn);

            var products = repoProd.GetAllProducts();

            Console.WriteLine("Here is a list of all of the Products with their Name, Price, and CategoryID.");
            foreach (var prod in products)
            {
                Console.WriteLine($" {prod.Name}, ${prod.Price}, & {prod.CategoryID}");
            }

            //Some nested loops, null checks, and even a little TryParse action to have a little fun with adding a product.
            bool newProduct = true;
            bool anotherProduct;

            Console.WriteLine("Would you like to add a product: Yes or No?");
            var addProduct = Console.ReadLine();

            if (addProduct == null)
            {
                do
                {
                    Console.WriteLine("Please try again.");
                    Console.WriteLine("Would you like to add a product: Yes or No?");
                    addProduct = Console.ReadLine();

                } while (addProduct == null); 
            } 
            
            else if (addProduct.ToLower() == "yes")
            {
                newProduct = true;
            }   
            else
            {
                newProduct = false;
            }

            do
            {
                do
                {
                    Console.WriteLine("Add/Type a new Product starting with the Product Name.");
                    var newProductName = Console.ReadLine();

                    if (newProductName == null || newProductName == "")
                    {
                        do
                        {
                            Console.WriteLine("Please try again.");
                            Console.WriteLine("Would you like to add a product: Yes or No?");
                            newProductName = Console.ReadLine();

                        } while (newProductName == null);
                    }

                    bool actualDouble;
                    double newProductPrice;

                    do
                    {
                        Console.WriteLine("What is the price of the product?");
                        actualDouble = double.TryParse(Console.ReadLine(), out newProductPrice);
                        Console.WriteLine("That is not a number. Please try again using numbers in this format: 00.00");

                    } while (!actualDouble);

                    bool actualInt;
                    int newProductCID;

                    do
                    {
                        Console.WriteLine("What is the Category ID of the product (1-10)?");
                        actualInt = int.TryParse(Console.ReadLine(), out newProductCID);
                        if (actualInt == false)
                        {
                            Console.WriteLine("That is not a number. Please try again using just numbers.");
                        }

                    } while (!actualInt);



                    repoProd.CreateProduct(newProductName, newProductPrice, newProductCID);
                    products = repoProd.GetAllProducts();

                    Console.WriteLine("Here is the list of products again with your new product added."); 
                    foreach (var prod in products)
                    {
                        Console.WriteLine($" {prod.Name}, {prod.Price}, & {prod.CategoryID}");
                    }

                    Console.WriteLine("Would you like to add another product: Yes or No?");
                    addProduct = Console.ReadLine();

                    if (addProduct == null)
                    {
                        do
                        {
                            Console.WriteLine("Please try again.");
                            Console.WriteLine("Would you like to add another product: Yes or No?");
                            addProduct = Console.ReadLine();

                        } while (addProduct == null);
                    }

                    if (addProduct.ToLower() == "yes")
                    {
                        anotherProduct = true;
                    }
                    else
                    {
                        anotherProduct = false;
                    }

                } while (anotherProduct);

            } while (newProduct);

            Console.WriteLine("Thank you for your time! Have a great day!"); 

        }
    }
}
