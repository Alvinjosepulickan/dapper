using Dapper;
using DapperDemo.Models;
using Microsoft.Data.SqlClient;
using System.ComponentModel.Design;
using System.Data;
using System.Net;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DapperDemo.Repository {
    public class EmployeeRepositoryDapper : IEmployeeRepository {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;
        public EmployeeRepositoryDapper(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection=new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public Employee Create(Employee employee) {
            var sql = "INSERT INTO Employees (Name, Title, Email, Phone, CompanyId) VALUES(@Name, @Title, @Email, @Phone, @CompanyId);"
                +"SELECT CAST(SCOPE_IDENTITY() as int); ";
            var id = _connection.Query<int>(sql, new { Name = employee.Name, @Title = employee.Title, @Email = employee.Email, @Phone = employee.Phone, @CompanyId = employee.CompanyId }).Single();
            employee.Id = id;
            return employee;
        }
        public bool Delete(int id) {
            var sql = "DELETE FROM Employees WHERE Id = @Id"; 
            _connection.Execute(sql, new { @Id = id});
            return true;
        }
        public List<Employee> GetAll() {
            var sql= "SELECT * FROM Employees";
            return _connection.Query<Employee>(sql).ToList();
        }
        public Employee GetById(int id) {
            var sql = "SELECT * FROM Employees WHERE Id = @Id";
            return _connection.Query<Employee>(sql, new { @Id = id }).FirstOrDefault();
        }

        public Employee Update(Employee employee) {
            var sql = "UPDATE Employees SET Name = @Name, Title = @Title, Email = @Email, Phone = @Phone, CompanyId = @CompanyId WHERE Id = @EmployeeId";
            _connection.Execute(sql, new { Name = employee.Name, @Title = employee.Title, @Email = employee.Email, @Phone = employee.Phone, @CompanyId = employee.CompanyId, @EmployeeId=employee.Id });
            return employee;
        }
    }
}
