using System.Net;
using FluentValidation;
using Foundation.Core.Extensions;
using TodoApp.Domain.Commands.Todo;

namespace TodoApp.Service.Validations.Commands.Todo
{
    public class DeleteTodoCommandValidator : AbstractValidator<DeleteTodoCommand>
    {
        public DeleteTodoCommandValidator()
        {
            RuleFor(todo => todo).NotEmpty().NotNull().WithErrorCode($"{HttpStatusCode.BadRequest}").WithMessage("No Todo found");
            RuleFor(todo => todo.Id).NotEmpty().NotNull().WithErrorCode($"{HttpStatusCode.BadRequest}").WithMessage("No Todo Id found");
        }
    }
}