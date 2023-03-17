using DapperDemo.Models;

namespace DapperDemo.Repository {
    public interface ICompanyRepository {
        Task<List<Company>> GetAll();
        Task<Company> GetById(int id);
        Task<Company> Create(Company company);
        Task<Company> Update(Company company);
        Task<bool> Delete(int id);
    }
}
