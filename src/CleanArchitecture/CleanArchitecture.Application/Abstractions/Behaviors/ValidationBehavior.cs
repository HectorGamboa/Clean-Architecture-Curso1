using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Application.Abstractions.Messging;
using CleanArchitecture.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace CleanArchitecture.Application.Abstractions.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
    {
        private readonly IEnumerable<IValidator<TRequest>> _validator;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            if(!_validator.Any()) return await next();
             var context =  new ValidationContext<TRequest>(request);
            var validationsErrors = _validator
             .Select(validators => validators.Validate(context))
             .Where(ValidationResult => ValidationResult.Errors.Any())
             .SelectMany(ValidationResult => ValidationResult.Errors)
             .Select(validationFailure => new ValidationError(validationFailure.PropertyName, validationFailure.ErrorMessage)).ToList();

             if(validationsErrors.Any()) {
                throw new Exceptions.ValidationException(validationsErrors);
             }
                return await next(); 
        }
    }
}