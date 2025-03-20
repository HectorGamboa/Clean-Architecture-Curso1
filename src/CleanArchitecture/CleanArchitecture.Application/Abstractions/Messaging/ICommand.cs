using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Abstractions;
using MediatR;

namespace CleanArchitecture.Application.Abstractions.Messging
{
    public interface ICommand : IRequest<Result>,IBaseCommand
    {
        
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>,IBaseCommand
    {
        
    }

    public interface IBaseCommand{

    }
}