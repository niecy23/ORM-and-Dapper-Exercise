using System;
using System.Data;
using System.Data.Common;
using Dapper;

namespace ORM_Dapper
{
	public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public DapperProductRepository(IDbConnection connection)
		{
            _connection = connection;
		}

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products;");
        }

        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO PRODUCTS (Name, Price, CategoryID) VALUES (@Name, @Price, @CategoryID);",
             new { name , price , categoryID });
        }

        public void UpdateProduct(int productID, string updatedName)
        {
            _connection.Execute("UPDATE products SET Name = @updatedName WHERE productID = @productID;",
                new { updatedName = updatedName, productID = productID });
        }

        public void DeleteProduct(int productID)
        {
            _connection.Execute("DELETE FROM products WHERE ProductID = @productID;",
                new { productID = productID });
            _connection.Execute("DELETE FROM sales WHERE ProductID = @productID;",
            new { productID = productID });
            _connection.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
            new { productID = productID });
        }
    }
}

