using System.Linq.Expressions;
using System.Net;
using FluentValidation;
using Foundation.Core.Extensions;
using TodoApp.Domain.Commands.Todo;
using TodoApp.Domain.Dtos;

namespace TodoApp.Service.Validations.Commands.Todo
{
    public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
    {
        public CreateTodoCommandValidator()
        {
            RuleFor(todo => todo).NotEmpty().NotNull().WithErrorCode($"{HttpStatusCode.BadRequest}").WithMessage("No Todo found");
            RuleFor(todo => todo.Title).NotEmpty().NotNull().WithErrorCode($"{HttpStatusCode.BadRequest}").WithMessage("No Todo Title found");
            RuleFor(todo => todo.Title).MinimumLength(11).WithErrorCode($"{HttpStatusCode.BadRequest}").WithMessage("Todo title must be longer than 10 characters");
            RuleFor(todo => todo.Deadline).NotNull().NotEmpty().WithErrorCode($"{HttpStatusCode.BadRequest}").WithMessage("No Todo Deadline found");
            RuleFor(todo => todo.Deadline).Must(BeValidTimeFrame).WithErrorCode($"{HttpStatusCode.BadRequest}").WithMessage("Invalid Todo Deadline");
        }
        private static bool BeValidTimeFrame(TimeFrameDto deadline)
        {
            return deadline.From < deadline.To;
        }
    }
}