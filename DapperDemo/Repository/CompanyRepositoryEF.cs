using DapperDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DapperDemo.Repository {
    public class CompanyRepositoryEF: ICompanyRepository {
        private readonly ApplicationDbContext _context;
        public CompanyRepositoryEF(ApplicationDbContext context)
        {
            _context = context;
        }

        public Company Create(Company company) {
            _context.Companies.Add(company);
            _context.SaveChanges();
            return company;
        }

        public bool Delete(int id) {
            var company = _context.Companies.Where(x => x.Id == id).FirstOrDefault();
            if(company !=null && company.Id>0) {
                _context.Companies.Remove(company);
                _context.SaveChanges();
                return  true;
            }
            return false;
        }

        public List<Company> GetAll() {
            return _context.Companies.ToList();
        }

        public Company GetById(int id) {
            var  company= _context.Companies.Where(x=>x.Id==id).FirstOrDefault();
            return company;
        }

        public Company Update(Company company) {
            _context.Companies.Update(company);
            _context.SaveChanges();
            return company;
        }
    }
}
