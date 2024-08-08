using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using miniProject_API.Data;
using miniProject_API.DTOs.DepartmentDTOs;
using miniProject_API.Entities;
using miniProject_API.Extension;
using miniProject_API.Helpers;

namespace miniProject_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly HospitalDbContext _hospitalDbContext;
        private readonly IMapper _mapper;

        public DepartmentController(HospitalDbContext hospitalDbContext, IMapper mapper)
        {
            _hospitalDbContext = hospitalDbContext;
            _mapper = mapper;
        }
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var departments = await _hospitalDbContext.Departments
                .Include(d => d.Doctors)
                .AsNoTracking()
                .ToListAsync();
            List<DepartmentReturnDTO> returnList = new();
            foreach (var department in departments)
            {
                returnList.Add(_mapper.Map<DepartmentReturnDTO>(department));
            }
            return Ok(returnList);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id is null) return BadRequest();
            var existDepartment = await _hospitalDbContext.Departments.Include(d => d.Doctors).AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
            if (existDepartment == null) return StatusCode(StatusCodes.Status404NotFound);
            return Ok(_mapper.Map<DepartmentReturnDTO>(existDepartment));
        }
        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] DepartmentCreateDTO departmentDTO)
        {
            if (await _hospitalDbContext.Departments.AnyAsync(g => g.Name.Trim().ToLower() == departmentDTO.Name.Trim().ToLower())) return BadRequest("Department name already exists");

            var department = _mapper.Map<Department>(departmentDTO);
            await _hospitalDbContext.Departments.AddAsync(department);
            await _hospitalDbContext.SaveChangesAsync();
            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DepartmentUpdateDTO departmentDTO)
        {
            var existDepartment = await _hospitalDbContext.Departments.FirstOrDefaultAsync(g => g.Id == id);
            if (existDepartment == null) return NotFound();
            if (existDepartment.Name != departmentDTO.Name && await _hospitalDbContext.Departments.AnyAsync(g => g.Name.Trim().ToLower() == departmentDTO.Name.Trim().ToLower() && g.Id != id)) return BadRequest("Department name already exists");
            existDepartment.Name = departmentDTO.Name;
            existDepartment.Limit = departmentDTO.Limit;
            if (departmentDTO.File != null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/images", existDepartment.Image);
                FileHelper.Delete(path);
                existDepartment.Image = departmentDTO.File.Save(Directory.GetCurrentDirectory(), "wwwroot/uploads/images");
            }
            await _hospitalDbContext.SaveChangesAsync();
            return Ok(existDepartment);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existDepartment = await _hospitalDbContext.Departments.FirstOrDefaultAsync(g => g.Id == id);
            if (existDepartment == null) return NotFound();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/images", existDepartment.Image);
            FileHelper.Delete(path);
            _hospitalDbContext.Departments.Remove(existDepartment);
            await _hospitalDbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
