﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.AppDBcontext;
using Data.Models;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Students>>> Getstudents()
        {
            return await _context.students.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Students>> GetStudents(Guid id)
        {
            var students = await _context.students.FindAsync(id);

            if (students == null)
            {
                return NotFound();
            }

            return students;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudents(Guid id, Students students)
        {
            if (id != students.ID)
            {
                return BadRequest();
            }

            _context.Entry(students).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Students>> PostStudents(Students students)
        {
            _context.students.Add(students);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudents", new { id = students.ID }, students);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudents(Guid id)
        {
            var students = await _context.students.FindAsync(id);
            if (students == null)
            {
                return NotFound();
            }

            _context.students.Remove(students);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentsExists(Guid id)
        {
            return _context.students.Any(e => e.ID == id);
        }
    }
}
