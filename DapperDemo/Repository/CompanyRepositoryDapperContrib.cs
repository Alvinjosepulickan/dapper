using Dapper;
using Dapper.Contrib.Extensions;
using DapperDemo.Models;
using Microsoft.Data.SqlClient;
using NuGet.Protocol.Plugins;
using System.ComponentModel.Design;
using System.Data;
using System.Net;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DapperDemo.Repository {
    public class CompanyRepositoryDapperContrib : ICompanyRepository {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;
        public CompanyRepositoryDapperContrib(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection=new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public Company Create(Company company) {
            _connection.Insert(company);

            return company;
        }
        public bool Delete(int id) {
            var company = GetById(id);
            _connection.Delete(company);
            return true;
        }
        public List<Company> GetAll() {
            return _connection.GetAll<Company>().ToList();
        }
        public Company GetById(int id) {
            return _connection.Get<Company>(id);
        }

        public Company Update(Company company) {
            _connection.Update(company);

            return company;
        }
    }
}
