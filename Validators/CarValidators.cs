using ApiTest1.Context;
using ApiTest1.Dtos;
using ApiTest1.Entities;
using FluentValidation;

namespace ApiTest1.Validators
{
    public class CreateCarValidator : AbstractValidator<CreateCarModel>
    {
        public CreateCarValidator(DatabaseContext databaseContext) 
        {
            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .NotNull().NotEmpty().WithMessage("{PropertyName} should not be null.")
                .Must(Model => databaseContext.Cars.Where(a => a.Model == Model).FirstOrDefault() == null)
                    .WithMessage("{PropertyName} must be unique.");

            RuleFor(x => x.Year)
                .NotEmpty()
                .NotNull()
                .Must(Year => Year > 1997)
                    .WithMessage("{PropertyName} must be greater than 1997.");

            RuleFor(x => x.Color)
                .NotEmpty()
                .NotNull();
        }
    }
    public class UpdateCarValidator : AbstractValidator<Car>
    {

    }
}
