﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using PaaspopService.Application.Infrastructure.Exceptions;

namespace PaaspopService.Application.Infrastructure.Requests
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<TRequest> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0) LogErrors(failures);

            return next();
        }

        public void LogErrors(List<ValidationFailure> failures)
        {
            var failuresString = new StringBuilder();
            foreach (var failure in failures)
            {
                failuresString.Append(failures.Last().Equals(failure) ? failure.ErrorMessage + " " : failure.ErrorMessage + " - ");
                _logger.LogError(failure.ErrorMessage);
            }
            throw new CustomValidationException(failuresString.ToString());
        }
    }
}