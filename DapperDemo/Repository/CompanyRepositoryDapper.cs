using Dapper;
using DapperDemo.Models;
using Microsoft.Data.SqlClient;
using System.ComponentModel.Design;
using System.Data;
using System.Net;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DapperDemo.Repository {
    public class CompanyRepositoryDapper : ICompanyRepository {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;
        public CompanyRepositoryDapper(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection=new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public Company Create(Company company) {
            var sql = "INSERT INTO Companies (Name, Address, City, State, PostalCode) VALUES(@Name, @Address, @City, @State, @PostalCode);"+
            "SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = _connection.Query<int>(sql, new { @Name = company.Name, @Address = company.Address, @City = company.City, @State = company.State, @PostalCode = company.PostalCode }).Single();
            company.Id = id;
            return company;
        }
        public bool Delete(int id) {
            var sql = "DELETE FROM Companies WHERE Id = @Id"; 
            _connection.Execute(sql, new { @Id = id});
            return true;
        }
        public List<Company> GetAll() {
            var sql= "SELECT * FROM Companies";
            return _connection.Query<Company>(sql).ToList();
        }
        public Company GetById(int id) {
            var sql = "SELECT * FROM Companies WHERE Id = @Id";
            return _connection.Query<Company>(sql, new { @Id = id }).FirstOrDefault();
        }

        public Company Update(Company company) {
            var sql = "UPDATE Companies SET Name = @Name, Address = @Address, City = @City, State = @State, PostalCode = @PostalCode WHERE CompanyId = @CompanyId";
            _connection.Execute(sql, new { @Name = company.Name, @Address = company.Address, @City = company.City, @State = company.State, @PostalCode = company.PostalCode });
            return company;
        }
    }
}
