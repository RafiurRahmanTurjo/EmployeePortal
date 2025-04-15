using EmployeePortalWeb.Data;
using EmployeePortalWeb.Models;
using Microsoft.AspNetCore.Mvc;
using EmployeePortalWeb.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortalWeb.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel ViewModel)
        {
            var employee = new Employee
            {
                Name = ViewModel.Name,
                Email = ViewModel.Email,
                PhoneNumber = ViewModel.PhoneNumber,
                SubscribedToNewsletter = ViewModel.SubscribedToNewsletter
            };
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var employees = await dbContext.Employees.ToListAsync();
            return View(employees);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var employees = await dbContext.Employees.FindAsync(id);
            return View(employees);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Employee ViewModel)
        {
            var employee = await dbContext.Employees.FindAsync(ViewModel.Id);
            if(employee is not null)
            {
                employee.Name = ViewModel.Name;
                employee.Email = ViewModel.Email;
                employee.PhoneNumber = ViewModel.PhoneNumber;
                employee.SubscribedToNewsletter = ViewModel.SubscribedToNewsletter;
            }
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List","Employee");
        }
        [HttpPost]
        public async Task<IActionResult>Delete(Employee ViewModel)
        {
            var employee = await dbContext.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(x=>x.Id == ViewModel.Id);
            if(employee is not null)
            {
                dbContext.Employees.Remove(ViewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Employee");
        }
        [HttpPost]
        public async Task<IActionResult> Add(Employee ViewModel)
        {
            return RedirectToAction("Add", "Employee");
        }
    }
}
