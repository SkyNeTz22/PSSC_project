using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace PSSCProject.Domain.Models
{
    public record ProductId
    {
        public decimal Value { get; }

        internal ProductId(decimal value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidProductException($"{value:0.##} is an invalid product ID.");
            }
        }

        public override string ToString()
        {
            return $"{Value:0.##}";
        }

        public static Option<ProductId> TryParseId(string productString)
        {
            if (decimal.TryParse(productString, out decimal numericId) && IsValid(numericId))
            {
                return Some<ProductId>(new(numericId));
            }
            else
            {
                return None;
            }
        }

        private static bool IsValid(decimal numericGrade) => numericGrade > 0 && numericGrade <= 10;
    }
}
