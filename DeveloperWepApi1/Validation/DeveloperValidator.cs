using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.RequestModels;
using FluentValidation;

namespace DeveloperWepApi1.Validation
{
    public class DeveloperValidator : AbstractValidator<Developer>
    {
        public DeveloperValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("name boş olamaz"); 
            RuleFor(x => x.Name).MaximumLength(15).WithMessage("15 den büyük olamaz");

            RuleFor(x => x.Surname).NotNull().WithMessage("soyadı boş olamaz");
            RuleFor(x => x.Surname).MaximumLength(10).WithMessage("10 karaterden fazla olamaz");
            
        }
    }
}