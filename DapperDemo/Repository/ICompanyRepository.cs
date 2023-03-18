using DapperDemo.Models;

namespace DapperDemo.Repository {
    public interface ICompanyRepository {
        List<Company> GetAll();
        Company GetById(int id);
        Company Create(Company company);
        Company Update(Company company);
        bool Delete(int id);
    }
}
