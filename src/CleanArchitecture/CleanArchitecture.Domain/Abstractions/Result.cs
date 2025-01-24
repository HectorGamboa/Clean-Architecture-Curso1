using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Abstractions
{
    public class Result
    {
        protected internal Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
            {
                throw new InvalidOperationException();
            }
            if (!isSuccess && error == Error.None)
            {
                throw new InvalidOperationException();
            }
            IsSuccess = isSuccess;
            Error = error;
        }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error? Error { get; }
        public static Result Success() => new Result(true, Error.None);
        public static Result Failure(Error error) => new Result(false, error);

        public static Result<TValue> Susccess<TValue>(TValue value) 
            => new Result<TValue>(value, true, Error.None);
        public static Result<TValue> Failure<TValue>(Error error)
            => new Result<TValue>(default, false, error);

        public static Result<TValue> Create<TValue>(TValue value)
            => value is not null
                ? Susccess(value)
                : Failure<TValue>(Error.NullValue);
    }


    public class Result<TValue>:Result{
        private readonly TValue _value;

        protected internal Result(TValue? value, bool isSuccess, Error error):base(isSuccess,error) {
            _value = value!;
        }
        [NotNull]
        public TValue Value => IsSuccess 
                                    ? _value! 
                                    : throw new InvalidOperationException("El valor del resultado no es admisible");

        public static implicit operator Result<TValue>(TValue value) => Create(value);
    }
}