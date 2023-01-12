using LanguageExt;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using LanguageExt.Common;
using PSSCProject.Domain.Repositories;

namespace PSSCProject.Data.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ContextForProducts dbContext;

        public ProductsRepository(ContextForProducts dbContext)
        {
            this.dbContext = dbContext;
        }

        public TryAsync<List<ProductsAmount>> TryGetExistingProductsByCategory() => async () => (await (
                          from prods in dbContext.Products
                          select new { prods.Id, prods.Category, prods.Name, prods.Price, prods.Stoc })
                          .AsNoTracking()
                          .ToListAsync())
                          .Select(result => new ProductsAmount(
                                                    Category: new(result.ToString()),
                                                    Name: new(result.Name.ToString()),
                                                    Price: new(result.Price),
                                                    Stoc: new(result.Stoc)
                            {
                                productsId = result.Id
                            })
                            .ToList());

        public TryAsync<Unit> TrySaveProducts(CurrentProducts products) => async () =>
        {
            var students = (await dbContext.Students.ToListAsync()).ToLookup(student => student.RegistrationNumber);
            var newGrades = grades.GradeList
                                    .Where(g => g.IsUpdated && g.GradeId == 0)
                                    .Select(g => new GradeDto()
                                    {
                                        StudentId = students[g.StudentRegistrationNumber.Value].Single().StudentId,
                                        Exam = g.ExamGrade.Value,
                                        Activity = g.ActivityGrade.Value,
                                        Final = g.FinalGrade.Value,
                                    });
            var updatedGrades = grades.GradeList.Where(g => g.IsUpdated && g.GradeId > 0)
                                    .Select(g => new GradeDto()
                                    {
                                        GradeId = g.GradeId,
                                        StudentId = students[g.StudentRegistrationNumber.Value].Single().StudentId,
                                        Exam = g.ExamGrade.Value,
                                        Activity = g.ActivityGrade.Value,
                                        Final = g.FinalGrade.Value,
                                    });

            dbContext.AddRange(newGrades);
            foreach (var entity in updatedGrades)
            {
                dbContext.Entry(entity).State = EntityState.Modified;
            }

            await dbContext.SaveChangesAsync();

            return unit;
        };
    }
}
