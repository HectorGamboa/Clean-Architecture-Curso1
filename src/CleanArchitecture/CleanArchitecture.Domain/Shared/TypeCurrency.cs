using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Shared
{
    public record TypeCurrency{
        public static readonly TypeCurrency Usd = new TypeCurrency("USD");
        public static readonly TypeCurrency Eur = new TypeCurrency("EUR");
        public static readonly TypeCurrency None = new TypeCurrency("");
        private TypeCurrency( string code) => Code = code;
        public string? Code { get; init; }
        public static readonly IReadOnlyCollection<TypeCurrency> All = [
            Usd,
            Eur
        ];
        public static TypeCurrency FromCode(string code){
            return All.FirstOrDefault(x => x.Code == code) ?? 
                throw new ArgumentException($"Invalid currency code: {code}");
        }
    }
}