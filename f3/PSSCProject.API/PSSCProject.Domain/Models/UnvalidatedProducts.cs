using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSSCProject.Domain.Models
{
    public record UnvalidatedProducts(int ProductId, string Category, string Name, decimal Price, int Stoc);
}
