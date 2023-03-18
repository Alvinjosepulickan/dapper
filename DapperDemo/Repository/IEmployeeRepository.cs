using DapperDemo.Models;

namespace DapperDemo.Repository {
    public interface IEmployeeRepository {
        List<Employee> GetAll();
        Employee GetById(int id);
        Employee Create(Employee employee);
        Employee Update(Employee employee);
        bool Delete(int id);
    }
}
