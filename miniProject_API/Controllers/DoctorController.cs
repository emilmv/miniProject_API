using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using miniProject_API.Data;
using miniProject_API.DTOs.DepartmentDTOs;
using miniProject_API.DTOs.DoctorDTOs;
using miniProject_API.Entities;
using miniProject_API.Extension;
using miniProject_API.Helpers;

namespace miniProject_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly HospitalDbContext _hospitalDbContext;
        private readonly IMapper _mapper;
        public DoctorController(HospitalDbContext hospitalDbContext, IMapper mapper)
        {
            _hospitalDbContext = hospitalDbContext;
            _mapper = mapper;
        }
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var doctors = await _hospitalDbContext.Doctors
                .Include(d => d.Department)
                .AsNoTracking()
                .ToListAsync();
            List<DoctorReturnDTO> returnList = new();
            foreach (var doctor in doctors)
            {
                returnList.Add(_mapper.Map<DoctorReturnDTO>(doctor));
            }
            return Ok(returnList);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var doctor = await _hospitalDbContext.Doctors
                .Include(d => d.Department)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
            if (doctor is null) return NotFound();
            return Ok(_mapper.Map<DoctorReturnDTO>(doctor));
        }
        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] DoctorCreateDTO doctorDTO)
        {
            if (await _hospitalDbContext.Doctors.AnyAsync(g => g.Name.Trim().ToLower() == doctorDTO.Name.Trim().ToLower())) return BadRequest("Doctor already exists");

            var doctor = _mapper.Map<Doctor>(doctorDTO);
            await _hospitalDbContext.Doctors.AddAsync(doctor);
            await _hospitalDbContext.SaveChangesAsync();
            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] DoctorUpdateDTO doctorDTO)
        {
            var existDoctor = await _hospitalDbContext.Doctors.FirstOrDefaultAsync(d => d.Id == id);
            if (existDoctor == null) return NotFound();
            if (existDoctor.Name != doctorDTO.Name && await _hospitalDbContext.Doctors.AnyAsync(d => d.Name.Trim().ToLower() == doctorDTO.Name.Trim().ToLower() && d.Id != id)) return BadRequest("Doctor with same name already exists");
            existDoctor.Name = doctorDTO.Name;
            existDoctor.ExperienceYear = doctorDTO.ExperienceYear;
            await _hospitalDbContext.SaveChangesAsync();
            return Ok(existDoctor);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var existDoctor = await _hospitalDbContext.Doctors.FirstOrDefaultAsync(d => d.Id == id);
            if (existDoctor == null) return NotFound();
            _hospitalDbContext.Doctors.Remove(existDoctor);
            await _hospitalDbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
