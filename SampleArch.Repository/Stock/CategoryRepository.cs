using SampleArch.Model;
using SampleArch.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Repository.Stock
{

    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext context)
            : base(context)
        {
        }


        public IEnumerable<Category> ListForSearch()
        {
            SampleArchContext ctx = _entities as SampleArchContext;

            var data = ctx.Categories.Select( p => new Category() {
                Id = p.Id,
                Description = p.Description,
                CompleteKey = p.CompleteKey
            });

            return data.ToList();           
        }
    }
}
