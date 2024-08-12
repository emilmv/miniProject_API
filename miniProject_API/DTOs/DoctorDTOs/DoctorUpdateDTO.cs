using FluentValidation;
using miniProject_API.DTOs.DepartmentDTOs;

namespace miniProject_API.DTOs.DoctorDTOs
{
    public class DoctorUpdateDTO
    {
        public string Name { get; set; }
        public int ExperienceYear { get; set; }
        public int DepartmentID { get; set; }
    }
    public class DoctorUpdateDTOValidator : AbstractValidator<DoctorUpdateDTO>
    {
        public DoctorUpdateDTOValidator()
        {
            RuleFor(d => d.Name)
                .NotNull()
                .MaximumLength(10)
                .WithMessage("Doctor's name is too large")
                .MinimumLength(3)
                .WithMessage("Doctor's name is too small");
            RuleFor(d => d.ExperienceYear)
                .InclusiveBetween(1, 50)
                .WithMessage("Experience year range do not meet requirements");
            RuleFor(d => d.DepartmentID)
                .NotEmpty();
        }
    }
}
