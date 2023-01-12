using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSSCProject.Domain.Models
{
    public record ProductsInDb(ProductId Id, Grade ExamGrade, Grade ActivityGrade, Grade FinalGrade)
    {
        public int ProductId { get; set; }
    }
}
