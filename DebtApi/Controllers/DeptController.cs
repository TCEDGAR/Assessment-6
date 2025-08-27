using DeptApi.Context;
using DeptApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DeptApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeptController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DeptController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dept>>> GetDepartments() =>
            await _context.Departments.Include(d => d.Manager).ToListAsync();

        private bool CheckAuth(HttpRequest request)
        {
            if (!request.Headers.ContainsKey("Authorization")) return false;
            var header = request.Headers["Authorization"].ToString();
            if (!header.StartsWith("Basic ")) return false;

            var encoded = header.Substring("Basic ".Length).Trim();
            var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
            var parts = decoded.Split(':');
            return parts[0] == "admin" && parts[1] == "Admin@1234";
        }

        [HttpPost]
        public async Task<ActionResult<Dept>> PostDept(Dept dept)
        {
            if (!CheckAuth(Request)) return Unauthorized();
            _context.Departments.Add(dept);
            await _context.SaveChangesAsync();
            return dept;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDept(int id, Dept dept)
        {
            if (!CheckAuth(Request)) return Unauthorized();

            if (id != dept.DeptId) return BadRequest();

            _context.Entry(dept).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDept(int id)
        {
            if (!CheckAuth(Request)) return Unauthorized();

            var dept = await _context.Departments.FindAsync(id);
            if (dept == null) return NotFound();

            _context.Departments.Remove(dept);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }


}
    
