using FluentValidation;

namespace miniProject_API.DTOs.DoctorDTOs
{
    public class DoctorCreateDTO
    {
        public string Name { get; set; }
        public int ExperienceYear { get; set; }
        public int DepartmentId { get; set; }

        public class DoctorCreateDTOValidator : AbstractValidator<DoctorCreateDTO>
        {
            public DoctorCreateDTOValidator()
            {
                RuleFor(d => d.Name)
                    .NotNull()
                    .MaximumLength(20)
                    .WithMessage("Doctor name too large")
                    .MinimumLength(1)
                    .WithMessage("Doctor name has to be larger than 1 charachter");
                RuleFor(d => d.ExperienceYear)
                    .InclusiveBetween(1, 50)
                    .WithMessage("Experience year range do not meet requirements");
                RuleFor(d => d.DepartmentId)
                    .NotEmpty();
            }
        }
    }
}
