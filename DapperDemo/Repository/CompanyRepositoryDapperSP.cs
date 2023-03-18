using Dapper;
using DapperDemo.Models;
using Microsoft.Data.SqlClient;
using System.ComponentModel.Design;
using System.Data;
using System.Net;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DapperDemo.Repository {
    public class CompanyRepositoryDapperSP : ICompanyRepository {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;
        public CompanyRepositoryDapperSP(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection=new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public Company Create(Company company) {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", 0, DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Name", company.Name);
            parameters.Add("@Address", company.Address);
            parameters.Add("@City", company.City);
            parameters.Add("@State", company.State);
            parameters.Add("@PostalCode", company.PostalCode);
            this._connection.Execute("usp_AddCompany", parameters, commandType: CommandType.StoredProcedure);
            company.Id = parameters.Get<int>("CompanyId");

            return company;
        }
        public bool Delete(int id) {
            _connection.Execute("usp_RemoveCompany", new { CompanyId = id }, commandType: CommandType.StoredProcedure);
            return true;
        }
        public List<Company> GetAll() {
            return _connection.Query<Company>("usp_GetALLCompany",commandType:CommandType.StoredProcedure).ToList();
        }
        public Company GetById(int id) {
            return _connection.Query<Company>("usp_GetCompany", new { CompanyId = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public Company Update(Company company) {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", company.Id, DbType.Int32);
            parameters.Add("@Name", company.Name);
            parameters.Add("@Address", company.Address);
            parameters.Add("@City", company.City);
            parameters.Add("@State", company.State);
            parameters.Add("@PostalCode", company.PostalCode);
            this._connection.Execute("usp_UpdateCompany", parameters, commandType: CommandType.StoredProcedure);

            return company;
        }
    }
}
