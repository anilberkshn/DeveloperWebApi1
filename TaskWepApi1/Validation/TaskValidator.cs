using FluentValidation;
using TaskWepApi1.Model.TaskRequestModels;

namespace TaskWepApi1.Validation
{
    public class TaskValidator : AbstractValidator<CreateTaskDto>
    {
        public TaskValidator()
        {
            RuleFor(x => x.Title).NotNull().WithMessage("The title cannot be empty"); 
            RuleFor(x => x.Status).NotNull().WithMessage("The status cannot be empty");
            RuleFor(x => x.Description).NotNull().WithMessage("The description cannot be empty");
                
            RuleFor(x => x.Title).MaximumLength(50).WithMessage("The title cannot be more than 50 characters");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("The description cannot be more than 500 characters");
            RuleFor(x => x.Status.ToString()).MaximumLength(1).WithMessage("The description cannot be more than 500 characters");
        }
    }
}  