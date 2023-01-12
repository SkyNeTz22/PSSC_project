using LanguageExt.ClassInstances;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSSCProject.Domain.Models
{
    public record RetrievedProductsList(Category Category, string Name, decimal Price, int Stoc) { 
        public int ProductId { get; set; }
    }
}
