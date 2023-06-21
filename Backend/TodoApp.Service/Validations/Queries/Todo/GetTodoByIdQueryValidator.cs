using System.Net;
using FluentValidation;
using Foundation.Core.Extensions;
using TodoApp.Domain.Queries.Todo;

namespace TodoApp.Service.Validations.Queries.Todo
{
    public class GetTodoByIdQueryValidator : AbstractValidator<GetTodoByIdQuery>
    {
        public GetTodoByIdQueryValidator()
        {
            RuleFor(todo => todo).NotEmpty().NotNull().WithErrorCode($"{HttpStatusCode.BadRequest}").WithMessage("No Todo found");
            RuleFor(todo => todo.Id).NotEmpty().NotNull().WithErrorCode($"{HttpStatusCode.BadRequest}").WithMessage("No Todo Id found");
        }
    }
}