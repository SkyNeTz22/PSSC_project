using LanguageExt.ClassInstances.Pred;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;
using System.Text.RegularExpressions;

namespace PSSCProject.Domain.Models
{
    public record Category
    {
        // Ana Are Mere
        public string CategoryName { get; }
        public const string Pattern = "^\\w+$";
        private static readonly Regex PatternRegex = new(Pattern);

        public Category(string catName)
        {
            if (IsValid(catName))
            {
                CategoryName = catName;
            }
            else
            {
                throw new InvalidCategoryExeption("Categorie invalida.");
            }
        }

        private static bool IsValid(string categoryValue) => PatternRegex.IsMatch(categoryValue);

        public override string ToString()
        {
            return CategoryName;
        }

        public static Option<Category> TryParse(string stringValue)
        {
            if (IsValid(stringValue))
            {
                return Some<Category>(new(stringValue));
            }
            else
            {
                return None;
            }
        }
    }
    
}

