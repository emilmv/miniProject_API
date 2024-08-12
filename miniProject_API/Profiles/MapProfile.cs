using AutoMapper;
using miniProject_API.DTOs.DepartmentDTOs;
using miniProject_API.DTOs.DoctorDTOs;
using miniProject_API.Entities;
using miniProject_API.Extension;
using System.Text.RegularExpressions;

namespace miniProject_API.Profiles
{
    public class MapProfile:Profile
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MapProfile(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            var uriBuilder = new UriBuilder(
                _httpContextAccessor.HttpContext.Request.Scheme,
                _httpContextAccessor.HttpContext.Request.Host.Host,
                (int)_httpContextAccessor.HttpContext.Request.Host.Port
                );
            var url = uriBuilder.Uri.AbsoluteUri;

            //File Upload Map
            CreateMap<DepartmentCreateDTO, Department>()
               .ForMember(d => d.Image, map => map.MapFrom(dto => dto.File.Save(Directory.GetCurrentDirectory(), "wwwroot/uploads/images")));
            CreateMap<DepartmentUpdateDTO, Department>()
                .ForMember(d => d.Image, map => map.MapFrom(dto => dto.File.Save(Directory.GetCurrentDirectory(), "wwwroot/uploads/images")));
            //Doctor Map
            CreateMap<Doctor,DoctorInDepartmentReturnDTO>();
            CreateMap<Doctor, DoctorReturnDTO>();
            CreateMap<DoctorCreateDTO,Doctor>();
            //Department Map

            CreateMap<Department, DepartmentReturnDTO>()
                .ForMember(d=>d.Image,map=>map.MapFrom(s=>url+"uploads/images/"+s.Image));
                //.ForMember(d => d.Doctors, map => map.MapFrom(s => s.Doctors));


        } 
    }
}
