using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Vehicles
{
    public record Currency(decimal Amount,TypeCurrency TypeCurrency){
        public  static Currency operator +(Currency frist, Currency second) {
            if(frist.TypeCurrency != second.TypeCurrency){
                throw new InvalidOperationException("Cannot add two currencies of different types");
            }
            return new Currency(frist.Amount + second.Amount, frist.TypeCurrency);
        }  
        public static Currency Zero () => new(0, TypeCurrency.None);
        public  static Currency Zero (TypeCurrency Currency) => new(0, Currency);
        public  bool IsZero() => this == Zero(TypeCurrency);
    }
   

}