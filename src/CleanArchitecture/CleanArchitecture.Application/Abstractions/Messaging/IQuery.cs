using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Abstractions;
using MediatR;

namespace CleanArchitecture.Application.Abstractions.Messging
{
    public interface IQuery<TResponse>:IRequest<Result<TResponse>>
    {
        
    }
}