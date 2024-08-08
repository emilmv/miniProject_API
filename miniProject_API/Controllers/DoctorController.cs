using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using miniProject_API.Data;
using miniProject_API.DTOs.DepartmentDTOs;
using miniProject_API.DTOs.DoctorDTOs;

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



    }
}
