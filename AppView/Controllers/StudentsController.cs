using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.AppDBcontext;
using Data.Models;
using Newtonsoft.Json;
using System.Text;
using Azure;

namespace AppView.Controllers
{
    public class StudentsController : Controller
    {
       

        public StudentsController()
        {
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            List <Students> students = new List<Students> ();   
            using(var httpClient  = new HttpClient())
            { 
                using(var reponse =await httpClient.GetAsync("https://localhost:7244/api/Students"))
                {
                    string apiRepo = await reponse.Content.ReadAsStringAsync();
                    students= JsonConvert.DeserializeObject <List<Students>>(apiRepo);
                }
            }
            return View(students);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Students students = new Students();
            using (var httpClient = new HttpClient())
            {
                using (var reponse = await httpClient.GetAsync("https://localhost:7244/api/Students/"+id))
                {
                   if( reponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiRepo = await reponse.Content.ReadAsStringAsync();
                        students = JsonConvert.DeserializeObject<Students>(apiRepo);
                    }
                }
            }
            return View(students);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Ten,Tuoi,Role,Email,Luong,TrangThai")] Students students)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(students), Encoding.UTF8, "application/json");
                    using (var reponse = await httpClient.PostAsync("https://localhost:7244/api/Students/", content))
                    {
                        if (reponse.IsSuccessStatusCode)
                        {
                            string apiResponse = await reponse.Content.ReadAsStringAsync();
                            students = JsonConvert.DeserializeObject<Students>(apiResponse);
                            // Optionally, you can redirect to a different action if creation was successful.
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            // Handle error response
                            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        }
                    }
                        
                }
            }
            return View(students);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Students students = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7244/api/Students/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        students = JsonConvert.DeserializeObject<Students>(apiResponse);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }

            return View(students);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Ten,Tuoi,Role,Email,Luong,TrangThai")] Students students)
        {
            if (id != students.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                using (var httpClient = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(students), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync($"https://localhost:7244/api/Students/{id}", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        }
                    }
                }
            }

            return View(students);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            Students students = new Students();
            using (var httpClient = new HttpClient())
            {
                using (var reponse = await httpClient.GetAsync("https://localhost:7244/api/Students/" + id))
                {
                    if (reponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiRepo = await reponse.Content.ReadAsStringAsync();
                        students = JsonConvert.DeserializeObject<Students>(apiRepo);
                    }
                }
            }
            return View(students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"https://localhost:7244/api/Students/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> StudentsExists(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7244/api/Students/{id}"))
                {
                    return response.IsSuccessStatusCode;
                }
            }
        }
    }
}
