using FluentValidation;

namespace miniProject_API.DTOs.DepartmentDTOs
{
    public class DepartmentCreateDTO
    {
        public string Name { get; set; }
        public int Limit { get; set; }
        public IFormFile File { get; set; }
    }
    public class DepartmentCreateDTOValidator : AbstractValidator<DepartmentCreateDTO>
    {
        public DepartmentCreateDTOValidator()
        {
            RuleFor(d => d.Name)
                .NotNull()
                .MaximumLength(20)
                .WithMessage("Department name too large")
                .MinimumLength(1)
                .WithMessage("Department name has to be larger than 1 charachter");
            RuleFor(d => d.Limit)
                .InclusiveBetween(1, 50)
                .WithMessage("Range do not meet requirements");
            RuleFor(d => d)
                .Custom((d, context) =>
                {
                    if (d.File is null)
                    {
                        context.AddFailure("File", "Please upload an image");
                        return;
                    }
                    if (d.File.Length / 1024 > 500) context.AddFailure("File", "Image size is too large");
                    if (!d.File.ContentType.Contains("image/")) context.AddFailure("File", "Upload has to be an image");
                });
        }
    }
}
