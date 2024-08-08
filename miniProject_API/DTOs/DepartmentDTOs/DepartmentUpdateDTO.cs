using FluentValidation;

namespace miniProject_API.DTOs.DepartmentDTOs
{
    public class DepartmentUpdateDTO
    {
        public string Name { get; set; }
        public int Limit { get; set; }
        public IFormFile File { get; set; }
    }
    public class DepartmentUpdateDTOValidator : AbstractValidator<DepartmentUpdateDTO>
    {
        public DepartmentUpdateDTOValidator()
        {
            RuleFor(g => g.Name)
                .MaximumLength(10)
                .WithMessage("Department name too large")
                .MinimumLength(3)
                .WithMessage("Department name too small");
            RuleFor(g => g.Limit)
                .ExclusiveBetween(1, 50)
                .WithMessage("Range do not meet requirements");
            RuleFor(g => g)
                .Custom((g, context) =>
                {
                    if (g.File != null)
                    {
                        if (g.File.Length / 1024 > 500) context.AddFailure("File", "Image size is too large");
                        if (!g.File.ContentType.Contains("image/")) context.AddFailure("Upload has to be an image");
                    }
                });
        }
    }
}
