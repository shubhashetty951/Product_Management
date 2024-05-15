using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace Simple_Project
{
    public class DapperRepository
    {
        private readonly string _connectionString;

        public DapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Product> GetProducts()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                const string sql = "SELECT * FROM Products";
                return connection.Query<Product>(sql);
            }
        }

        public void AddProduct(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                const string sql = "INSERT INTO Products (Name, Description, Price) VALUES (@Name, @Description, @Price)";
                connection.Execute(sql, new { product.Name, product.Description, product.Price });
            }
        }

        public Product GetProduct(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                const string sql = "SELECT * FROM Products WHERE Id = @Id";
                return connection.QuerySingleOrDefault<Product>(sql, new { Id = id });
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                const string sql = "UPDATE Products SET Name = @Name, Description = @Description, Price = @Price WHERE Id = @Id";
                connection.Execute(sql, new { product.Id, product.Name, product.Description, product.Price });
            }
        }

        public void DeleteProduct(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                const string sql = "DELETE FROM Products WHERE Id = @Id";
                connection.Execute(sql, new { Id = id });
            }
        }
    }
}
