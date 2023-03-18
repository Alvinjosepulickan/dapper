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
    public class CompaniesController : Controller {
        private readonly ICompanyRepository _context;

        public CompaniesController(ICompanyRepository context) {
            _context = context;
        }

        // GET: Companies
        public async Task<IActionResult> Index() {
            return View(_context.GetAll());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id) {
            int companyId = id.GetValueOrDefault();
            var company = _context.GetById(companyId);
            if (company == null) {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,City,State,PostalCode")] Company company) {
            _context.Create(company);
            return RedirectToAction(nameof(Index));
            //return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            int companyId = id.GetValueOrDefault();
            var company = _context.GetById(companyId);
            if (company == null) {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,City,State,PostalCode")] Company company) {
            if (id != company.Id) {
                return NotFound();
            }

            try {
                _context.Update(company);
            }
            catch (DbUpdateConcurrencyException) {
                if (!await (CompanyExists(company.Id))) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            int companyId = id.GetValueOrDefault();
            var company = _context.GetById(companyId);
            if (company == null) {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            _context.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CompanyExists(int id) {
            var company = _context.GetById(id);
            if (company != null && company.Id < 1) {
                return false;
            }
            return true;
        }
    }
}
