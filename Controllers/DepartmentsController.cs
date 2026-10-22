using DockerKubernetes1.Models;
using Microsoft.AspNetCore.Mvc;

namespace DockerKubernetes1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private static readonly List<Department> Departments =
        [
            new Department { Id = 1, Name = "HR" },
            new Department { Id = 2, Name = "IT" }
        ];

        [HttpGet]
        public ActionResult<IEnumerable<Department>> GetAll()
        {
            return Ok(Departments);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Department> GetById(int id)
        {
            var department = Departments.FirstOrDefault(d => d.Id == id);
            if (department is null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        [HttpPost]
        public ActionResult<Department> Create(Department department)
        {
            var nextId = Departments.Count == 0 ? 1 : Departments.Max(d => d.Id) + 1;
            department.Id = nextId;
            Departments.Add(department);

            return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Department department)
        {
            var existingDepartment = Departments.FirstOrDefault(d => d.Id == id);
            if (existingDepartment is null)
            {
                return NotFound();
            }

            existingDepartment.Name = department.Name;
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var existingDepartment = Departments.FirstOrDefault(d => d.Id == id);
            if (existingDepartment is null)
            {
                return NotFound();
            }

            Departments.Remove(existingDepartment);
            return NoContent();
        }
    }
}