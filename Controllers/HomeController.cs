using EF_DBFirst_Approach.Data;
using EF_DBFirst_Approach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DBFirst_Approach.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeDBContext _employeeDBContext;

        public HomeController(EmployeeDBContext employeeDB)
        {
            _employeeDBContext = employeeDB;
        }
        public IActionResult Index()
        {
            var _emplst = _employeeDBContext.tblEmployees.
                            Join(_employeeDBContext.tblSkills, e => e.SkillID, s => s.SkillID,
                            (e, s) => new EmployeeViewModel
                            {
                                EmployeeID = e.EmployeeID,
                                EmployeeName = e.EmployeeName,
                                PhoneNumber = e.PhoneNumber,
                                SkillID= s.SkillID,
                                Skill = s.Title,
                                YearsExperience = e.YearsExperience
                            }).ToList();
            IList<EmployeeViewModel> emplst = _emplst;
            return View(emplst);
        }

        public IActionResult Create()
        {
            List<tblSkill> skills = new List<tblSkill>();
            skills = (from s in _employeeDBContext.tblSkills select s).ToList();
            skills.Insert(0, new tblSkill { SkillID = 0, Title = "--Select Skill--" });
            ViewBag.message = skills;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel emp)
        {
            if (emp == null)
            {
                return NotFound();
            }

            await _employeeDBContext.AddAsync(new tblEmployee { EmployeeName = emp.EmployeeName, SkillID = emp.SkillID, PhoneNumber = emp.PhoneNumber, YearsExperience = emp.YearsExperience });

            await _employeeDBContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            List<tblSkill> skills = new List<tblSkill>();
            skills = (from s in _employeeDBContext.tblSkills select s).ToList();
            skills.Insert(0, new tblSkill { SkillID = 0, Title = "--Select Skill--" });
            ViewBag.message = skills;

            var _emplst = _employeeDBContext.tblEmployees.
                            Join(_employeeDBContext.tblSkills, e => e.SkillID, s => s.SkillID,
                            (e, s) => new EmployeeViewModel
                            {
                                EmployeeID = e.EmployeeID,
                                EmployeeName = e.EmployeeName,
                                PhoneNumber = e.PhoneNumber,
                                SkillID = s.SkillID,
                                Skill = s.Title,
                                YearsExperience = e.YearsExperience
                            }).ToList();

            var _emp = _emplst.Where(e => e.EmployeeID == id).FirstOrDefault();

            return View(_emp);
        }

        public IActionResult Edit(int id)
        {
            List<tblSkill> skills = new List<tblSkill>();
            skills = (from s in _employeeDBContext.tblSkills select s).ToList();
            skills.Insert(0, new tblSkill { SkillID = 0, Title = "--Select Skill--" });
            ViewBag.message = skills;

            var _emplst = _employeeDBContext.tblEmployees.
                            Join(_employeeDBContext.tblSkills, e => e.SkillID, s => s.SkillID,
                            (e, s) => new EmployeeViewModel
                            {
                                EmployeeID = e.EmployeeID,
                                EmployeeName = e.EmployeeName,
                                PhoneNumber = e.PhoneNumber,
                                SkillID = s.SkillID,
                                Skill = s.Title,
                                YearsExperience = e.YearsExperience
                            }).ToList();

            var _emp = _emplst.Where(e => e.EmployeeID == id).FirstOrDefault();

            return View(_emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel emp)
        {

            if (emp == null)
            {
                return NotFound();
            }            

            var _emp = _employeeDBContext.tblEmployees.Where(e => e.EmployeeID == emp.EmployeeID).FirstOrDefault();

            if (await TryUpdateModelAsync<tblEmployee>(_emp, "", i => i.EmployeeID, i => i.EmployeeName, i => i.PhoneNumber, i => i.SkillID, i => i.YearsExperience))
            {
                try
                {
                    await _employeeDBContext.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction("Index");
            }



            return View(_emp);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            List<tblSkill> skills = new List<tblSkill>();
            skills = (from s in _employeeDBContext.tblSkills select s).ToList();
            skills.Insert(0, new tblSkill { SkillID = 0, Title = "--Select Skill--" });
            ViewBag.message = skills;

            var _emplst = _employeeDBContext.tblEmployees.
                            Join(_employeeDBContext.tblSkills, e => e.SkillID, s => s.SkillID,
                            (e, s) => new EmployeeViewModel
                            {
                                EmployeeID = e.EmployeeID,
                                EmployeeName = e.EmployeeName,
                                PhoneNumber = e.PhoneNumber,
                                SkillID = s.SkillID,
                                Skill = s.Title,
                                YearsExperience = e.YearsExperience
                            }).ToList();

            var _emp = _emplst.Where(e => e.EmployeeID == id).FirstOrDefault();

            return View(_emp);
        }

        [HttpPost]        
        public async Task<IActionResult> Delete(EmployeeViewModel emp)
        {
            _employeeDBContext.Remove(new tblEmployee { EmployeeID=emp.EmployeeID, EmployeeName = emp.EmployeeName, SkillID = emp.SkillID, PhoneNumber = emp.PhoneNumber, YearsExperience = emp.YearsExperience });

            await _employeeDBContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
