using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DapperDemo;
using DapperDemo.Models;
using DapperDemo.Repository;
using System.ComponentModel.Design;

namespace DapperDemo.Controllers {
    public class EmployeesController : Controller {
        private readonly IEmployeeRepository _context;
        private readonly ICompanyRepository _company;
        [BindProperty]
        public Employee employee { get; set; }
        public EmployeesController(IEmployeeRepository context, ICompanyRepository company) {
            _context = context;
            _company = company;
        }

        // GET: Companies
        public async Task<IActionResult> Index() {
            return View(_context.GetAll());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id) {
            int employeeId = id.GetValueOrDefault();
            var employee = _context.GetById(employeeId);
            if (employee == null) {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Companies/Create
        [HttpGet]
        public IActionResult Create() {
            IEnumerable<SelectListItem> companyList = _company.GetAll().Select(i => new SelectListItem {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            ViewBag.CompanyList = companyList;
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("CreatePost")]
        public async Task<IActionResult> CreatePost() {
            _context.Create(employee);
            return RedirectToAction(nameof(Index));
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            int employeeId = id.GetValueOrDefault();
            var employee = _context.GetById(employeeId);
            if (employee == null) {
                return NotFound();
            }
            IEnumerable<SelectListItem> companyList = _company.GetAll().Select(i => new SelectListItem {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            ViewBag.CompanyList = companyList;
            return View(employee);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(int id) {
            if (id != employee.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(employee);
                }
                catch (DbUpdateConcurrencyException) {
                    if (!await (EmployeeExists(employee.Id))) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            int employeeId = id.GetValueOrDefault();
            var employee = _context.GetById(employeeId);
            if (employee == null) {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            _context.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EmployeeExists(int id) {
            var employee = _context.GetById(id);
            if (employee != null && employee.Id < 1) {
                return false;
            }
            return true;
        }
    }
}
